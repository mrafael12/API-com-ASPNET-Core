using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using APIWithASPNETCore.Model.Context;
using Microsoft.EntityFrameworkCore;
using APIWithASPNETCore.Service;
using Microsoft.Extensions.Logging;
using APIWithASPNETCore.Repository.Generic;
using Tapioca.HATEOAS;
using APIWithASPNETCore.Hypermedia;
using Microsoft.AspNetCore.Rewrite;
using APIWithASPNETCore.Security.Configuration;
using Microsoft.Extensions.Options;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace APIWithASPNETCore
{
    public class Startup
    {
        private readonly ILogger _logger;   
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _environment = environment;
            _logger = logger;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            //Execute Migrations
            //executeMigrations(connectionString);


            ////////////////
            //Security Token
            ////////////////
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguration();
            new ConfigureFromConfigurationOptions<TokenConfiguration>(
                _configuration.GetSection("TokenConfiguration")
            ).Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                //Validates the signing of a received token
                paramsValidation.ValidateIssuerSigningKey = true;

                //Checks if a received token is still valid
                paramsValidation.ValidateLifetime = true;

                //Tolerance time for the expiration of a token(used in case
                //of time synchronization problems between different
                //computers involved inthe communication process)
                paramsValidation.ClockSkew = TimeSpan.Zero;                            
            });

            //Enables the use of th token as a means of
            //authorizing access to this project1s resouces
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
            ////////////////////
            //Fim Security Token
            ////////////////////

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "text/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
            })
            .AddXmlSerializerFormatters()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                        
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ObjectContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ObjectContentResponseEnricherList.Add(new BookEnricher());
            services.AddSingleton(filterOptions);

            services.AddApiVersioning(option => option.ReportApiVersions = true);

            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "RestFul API With ASP.NET Core 2.2",
                        Version = "v1"
                    });
            });

            //Dependency Injection
            services.AddScoped<IPersonService, PersonServiceImpl>();

            //Não precisamos fazer mais dessa forma
            //services.AddScoped<IPersonRepository, PersonRepositoryImpl>();

            //Dependency Injection of GenericRepository
            services.AddScoped<IUserService, UserServiceImpl>();
            services.AddScoped<IBookService, BookServiceImpl>();

            services.AddScoped<IUserRepository, UserRepositoryImpl>();
            services.AddScoped<IPersonRepository, PersonRepositoryImpl>();
            services.AddScoped(typeof(IRepository<>),typeof(GenericRepository<>));
        }

        public void executeMigrations(string connectionString)
        {
            if(_environment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);
                    var evolve = new Evolve.Evolve("Evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();
                }
                catch(Exception ex)
                {
                    _logger.LogCritical("Database Migration failed", ex);
                    throw;
                }
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

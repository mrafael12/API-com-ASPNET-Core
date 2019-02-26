using System.Collections.Generic;
using APIWithASPNETCore.Model;
using APIWithASPNETCore.Repository.Generic;
using APIWithASPNETCore.Data.Converters;
using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Security.Configuration;
using System;
using System.Security.Claims;
using System.Security.Principal;
using System.IdentityModel.Tokens.Jwt;

namespace APIWithASPNETCore.Service
{
    public class UserServiceImpl : IUserService
    {
        private IUserRepository _repository;       
        private SigningConfigurations _signingConfigurations;
        private TokenConfiguration _tokenConfiguration;

        public UserServiceImpl(IUserRepository repository, SigningConfigurations signingConfigurations, TokenConfiguration tokenConfiguration)
        {
            _repository = repository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }

        public User Create(User user)
        {
           return _repository.Create(user);
        }

        public object FindByLogin(UserVO user)
        {
            bool credentialIsValid = false;
            if(user != null && !string.IsNullOrWhiteSpace(user.Login))
            {
                var baseUser = _repository.FindByLogin(user.Login);
                var accessEncrypted = MD5Crypt.Criptografar(user.AccessKey);
                credentialIsValid = (baseUser != null && user.Login == baseUser.Login && accessEncrypted == baseUser.AccessKey);
            }

            if(credentialIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Login, "Login"),
                        new []
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                        }
                    
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return SuccessObject(createDate, expirationDate, token);
            }
            else
            {
                return ExceptionObject();
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autheticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "Ok"
            };
        }
    }
}

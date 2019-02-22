using System.Collections.Generic;
using APIWithASPNETCore.Model;
using APIWithASPNETCore.Repository.Generic;
using APIWithASPNETCore.Data.Converters;
using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Model.Context;
using System.Linq;

namespace APIWithASPNETCore.Service
{
    public class UserRepositoryImpl : IUserRepository
    {        
        private readonly MySQLContext _context;

        public UserRepositoryImpl(MySQLContext context)
        {
            _context = context;            
        }        
                
        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}

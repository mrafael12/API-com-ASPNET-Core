using APIWithASPNETCore.Model;
using APIWithASPNETCore.Repository.Generic;
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

        public User Create(User user)
        {
            user.AccessKey = MD5Crypt.Criptografar(user.AccessKey);
            _context.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User FindByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}

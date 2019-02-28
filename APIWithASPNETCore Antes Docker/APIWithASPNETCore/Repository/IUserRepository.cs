using APIWithASPNETCore.Model;
using System.Collections.Generic;

namespace APIWithASPNETCore.Repository.Generic
{
    public interface IUserRepository
    {                
        User FindByLogin(string login);
        User Create(User user);
    }
}

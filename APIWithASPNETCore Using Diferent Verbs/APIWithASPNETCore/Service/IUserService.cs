using APIWithASPNETCore.Model;

namespace APIWithASPNETCore.Service
{
    public interface IUserService
    {        
        object FindByLogin(User user);        
    }
}

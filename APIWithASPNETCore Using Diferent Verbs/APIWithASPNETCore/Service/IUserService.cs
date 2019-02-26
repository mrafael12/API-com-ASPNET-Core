using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Model;

namespace APIWithASPNETCore.Service
{
    public interface IUserService
    {
        User Create(User user);
        object FindByLogin(UserVO user);        
    }
}

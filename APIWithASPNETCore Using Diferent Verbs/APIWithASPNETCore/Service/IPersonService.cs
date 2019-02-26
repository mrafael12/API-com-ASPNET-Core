using APIWithASPNETCore.Data.VO;
using System.Collections.Generic;

namespace APIWithASPNETCore.Service
{
    public interface IPersonService
    {
        PersonVO Create(PersonVO PersonVO);
        PersonVO FindById(long id);
        List<PersonVO> FindByName(string firstName, string lastName);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO PersonVO);
        void Delete(long id);
    }
}

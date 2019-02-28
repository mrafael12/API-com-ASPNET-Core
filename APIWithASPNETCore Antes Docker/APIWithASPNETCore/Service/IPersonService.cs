using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Model;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;

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
        PagedSearchDTO<PersonVO> FindWidthPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}

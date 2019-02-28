using APIWithASPNETCore.Model;
using System.Collections.Generic;

namespace APIWithASPNETCore.Repository.Generic
{
    public interface IPersonRepository : IRepository<Person>
    {        
        List<Person> FindByName(string firstName, string lastName);      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWithASPNETCore.Model;
using System.Threading;
using APIWithASPNETCore.Model.Context;
using APIWithASPNETCore.Repository;
using APIWithASPNETCore.Repository.Generic;

namespace APIWithASPNETCore.Service
{
    public class PersonServiceImpl : IPersonService
    {
        private IRepository<Person> _repository;

        public PersonServiceImpl(IRepository<Person> repository)
        {
            _repository = repository;
        }        

        public Person Create(Person person)
        {            
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }        

        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWithASPNETCore.Model;
using System.Threading;
using APIWithASPNETCore.Model.Context;
using APIWithASPNETCore.Repository;
using APIWithASPNETCore.Repository.Generic;
using APIWithASPNETCore.Data.Converters;
using APIWithASPNETCore.Data.VO;

namespace APIWithASPNETCore.Service
{
    public class PersonServiceImpl : IPersonService
    {
        private IPersonRepository _repository;
        private readonly PersonConverter _converter;

        public PersonServiceImpl(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }        

        public PersonVO Create(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }

        public List<PersonVO> FindByName(string firstName, string lastName)
        {
            return _converter.ParseList(_repository.FindByName(firstName,lastName));            
        }

        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO person)
        {
            var personEntity = _converter.Parse(person);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }        
    }
}

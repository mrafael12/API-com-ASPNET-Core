﻿using APIWithASPNETCore.Model;
using APIWithASPNETCore.Repository.Generic;
using APIWithASPNETCore.Model.Context;
using System.Linq;
using System.Collections.Generic;

namespace APIWithASPNETCore.Service
{
    public class PersonRepositoryImpl : GenericRepository<Person>, IPersonRepository
    {                
        public PersonRepositoryImpl(MySQLContext context) : base (context) {}
        
        public List<Person> FindByName(string firstName, string lastName)
        {
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName) && p.LastName.Contains(lastName)).ToList();
            }
            else if (string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.LastName.Contains(lastName)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstName) && string.IsNullOrEmpty(lastName))
            {
                return _context.Persons.Where(p => p.FirstName.Contains(firstName)).ToList();
            }
            else
            {
                return _context.Persons.ToList();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;

namespace UserGroup.DAL.EF
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonGroupsContext _context;

        public PersonRepository(PersonGroupsContext context)
        {
            _context = context;
        }

        public IEnumerable<Person> Get(bool includeGroup = false)
        {
            if (includeGroup)
                return _context.Person.Include(p => p.Group)
                    .OrderBy(p => p.DateAdded).ToList();


            return _context.Person.OrderBy(p => p.DateAdded).ToList();
        }

        public IEnumerable<Person> GetByGroup(int groupId, bool includeGroup = false)
        {
            if (includeGroup)
                return _context.Person.Include(p => p.Group)
                    .Where(p => p.GroupId == groupId).OrderBy(p => p.DateAdded).ToList();

            return _context.Person
                .Where(p => p.GroupId == groupId)
                .OrderBy(p => p.DateAdded).ToList();
        }

        public Person Get(int id, bool includeGroup = false)
        {
            if (includeGroup)
                return _context.Person
                    .Include(p => p.Group)
                    .FirstOrDefault(p => p.Id == id);

            return _context.Person
                .FirstOrDefault(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Group.Any(g => g.Id == id);
        }

        public void Add(Person person)
        {
            _context.Person.Add(person);
        }

        public void Update(Person person)
        {
            _context.Person.Update(person);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public void Delete(Person person)
        {
            _context.Person.Remove(person);
        }
    }
}
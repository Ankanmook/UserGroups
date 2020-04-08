using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;

namespace UserGroup.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository )
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> Get()
        {
            return _personRepository.Get(true);
        }

        public Person Get(int id)
        {
            return _personRepository.Get(id, true);
        }
    }
}

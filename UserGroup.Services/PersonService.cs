using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IGroupService _groupService;

        public PersonService(IPersonRepository personRepository, IGroupService groupService)
        {
            _personRepository = personRepository;
            _groupService = groupService;
        }

        public IEnumerable<Person> Get(ResourceParameters resourceParameters)
        {
            return _personRepository.Get(resourceParameters, true);
        }

        public Person Get(int id)
        {
            return _personRepository.Get(id, true);
        }

        public bool Exists(int id)
        {
            return _personRepository.Exists(id);
        }

        public bool GroupExists(int groupId)
        {
            return _groupService.Exists(groupId);
        }

        public void Add(Person person)
        {
            _personRepository.Add(person);
        }

        public void Update(Person person)
        {
            _personRepository.Update(person);
        }

        public void Delete(Person person)
        {
            _personRepository.Delete(person);
        }

        public bool Save()
        {
            return _personRepository.Save();
        }
    }
}
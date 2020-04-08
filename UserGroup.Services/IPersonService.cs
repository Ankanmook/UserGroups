using System.Collections.Generic;
using UseGroup.DataModel.Models;

namespace UserGroup.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> Get();

        Person Get(int id);

        bool Exists(int id);

        bool GroupExists(int groupId);

        void Add(Person person);

        void Update(Person person);

        void Delete(Person person);

        bool Save();
    }
}
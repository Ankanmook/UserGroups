using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Contracts
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Get(ResourceParameters resourceParameters, bool includeGroup = false);
        Person Get(int id, bool includeGroup = false);
        IEnumerable<Person> GetByGroup(int groupId, bool includeGroup = false);
        bool Exists(int id);

        bool Save();
        void Add(Person person);
        void Update(Person person);

        void Delete(Person person);
    }
}

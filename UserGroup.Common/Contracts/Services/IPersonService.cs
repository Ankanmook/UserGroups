using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Contracts
{
    public interface IPersonService
    {
        IEnumerable<Person> Get(ResourceParameters resourceParameters);

        Person Get(int id);

        bool Exists(int id);

        bool GroupExists(int groupId);

        void Add(Person person);

        void Update(Person person);

        void Delete(Person person);

        bool Save();
    }
}
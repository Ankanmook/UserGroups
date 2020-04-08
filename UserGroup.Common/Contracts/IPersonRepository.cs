using System.Collections.Generic;
using UseGroup.DataModel.Models;

namespace UserGroup.Common.Contracts
{
    public interface IPersonRepository
    {
        IEnumerable<Person> Get(bool includeGroup = false);
        Person Get(int id, bool includeGroup = false);
        IEnumerable<Person> GetByGroup(int groupId, bool includeGroup = false);
    }
}

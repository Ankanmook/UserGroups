using System.Collections.Generic;
using UseGroup.DataModel.Models;

namespace UserGroup.Services
{
    public interface IGroupService
    {
        IEnumerable<Group> Get();

        Group Get(int id);

        bool Exists(int id);

        bool Add(Group group);

        bool Save();
    }
}
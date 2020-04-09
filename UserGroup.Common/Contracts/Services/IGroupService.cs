using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Contracts
{
    public interface IGroupService
    {
        IEnumerable<Group> Get(ResourceParameters resourceParameters);

        Group Get(int id);

        bool Exists(int id);

        bool Add(Group group);

        bool Save();
    }
}
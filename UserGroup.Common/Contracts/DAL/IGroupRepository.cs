using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Common.Contracts
{
    public interface IGroupRepository
    {
        IEnumerable<Group> Get(ResourceParameters resourceParameters);
        IEnumerable<Group> Get();
        Group Get(int id);

        bool Exists(int id);

        bool Save();
        void Add(Group group);
        bool GroupNameExists(string name);
    }
}
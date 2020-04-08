using System.Collections.Generic;
using UseGroup.DataModel.Models;

namespace UserGroup.Common.Contracts
{
    public interface IGroupRepository
    {
        IEnumerable<Group> Get();

        Group Get(int id);

        bool Exists(int id);

        bool Save();
        void Add(Group group);
        bool GroupNameExists(string name);
    }
}
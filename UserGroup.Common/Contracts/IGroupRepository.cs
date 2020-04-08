using System.Collections.Generic;
using UseGroup.DataModel.Models;

namespace UserGroup.Common.Contracts
{
    public interface IGroupRepository
    {
        IEnumerable<Group> Get();

        Group Get(int id);
    }
}
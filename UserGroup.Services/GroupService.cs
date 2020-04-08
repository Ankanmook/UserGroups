using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;

namespace UserGroup.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IEnumerable<Group> Get()
        {
            return _groupRepository.Get();
        }

        public Group Get(int id)
        {
            return _groupRepository.Get(id);
        }
    }
}

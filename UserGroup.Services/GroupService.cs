using System.Collections.Generic;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;
using UserGroup.DataModel.Helpers;

namespace UserGroup.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IEnumerable<Group> Get(ResourceParameters resourceParameters)
        {
            return _groupRepository.Get(resourceParameters);
        }

        public IEnumerable<Group> Get()
        {
            return _groupRepository.Get();
        }

        public Group Get(int id)
        {
            return _groupRepository.Get(id);
        }

        public bool Exists(int id)
        {
            return _groupRepository.Exists(id);
        }

        /// <summary>
        /// Checks the unique key in group name
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public bool Add(Group group)
        {
            if (_groupRepository.GroupNameExists(group.Name))
            {
                return false;
            }
            _groupRepository.Add(group);
            return true;
        }

        public bool Save()
        {
            return _groupRepository.Save();
        }
    }
}
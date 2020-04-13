using System.Collections.Generic;
using System.Linq;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;
using UserGroup.DataModel.Helpers;

namespace UserGroup.DAL
{
    public class GroupRepository : IGroupRepository
    {
        private readonly PersonGroupsContext _context;

        public GroupRepository(PersonGroupsContext context)
        {
            _context = context;
        }

        public IEnumerable<Group> Get(ResourceParameters resourceParameters)
        {
            return _context.Group
                .Skip(resourceParameters.PageSize * (resourceParameters.PageNumber - 1))
                .ToList();
        }


        public IEnumerable<Group> Get()
        {
            return _context.Group
                .ToList();
        }

        public Group Get(int id)
        {
            return _context.Group.FirstOrDefault(g => g.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Group.Any(g => g.Id == id);
        }

        public void Add(Group group)
        {
            _context.Group.Add(group);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public bool GroupNameExists(string name)
        {
            //very inefficient string lowecase matching 
            return _context.Group.Any(g => g.Name.ToLower() == name.ToLower());
        }
    }
}
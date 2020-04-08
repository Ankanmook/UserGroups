using System.Collections.Generic;
using System.Linq;
using UseGroup.DataModel.Models;
using UserGroup.Common.Contracts;

namespace UserGroup.DAL.EF
{
    public class GroupRepository : IGroupRepository
    {
        private readonly PersonGroupsContext _context;

        public GroupRepository(PersonGroupsContext context)
        {
            _context = context;
        }

        public IEnumerable<Group> Get()
        {
            return _context.Group.ToList();
        }

        public Group Get(int id)
        {
            return _context.Group.FirstOrDefault(g => g.Id == id);
        }
    }
}
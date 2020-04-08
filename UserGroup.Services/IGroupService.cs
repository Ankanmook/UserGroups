using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseGroup.DataModel.Models;

namespace UserGroup.Services
{
    public interface IGroupService
    {
        IEnumerable<Group> Get();
        Group Get(int id);
    }
}

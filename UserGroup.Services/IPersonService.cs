using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseGroup.DataModel.Models;

namespace UserGroup.Services
{
    public interface IPersonService
    {
        IEnumerable<Person> Get();

        Person Get(int id);
    }
}

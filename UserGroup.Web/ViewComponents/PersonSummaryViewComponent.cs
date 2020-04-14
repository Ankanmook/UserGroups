using Microsoft.AspNetCore.Mvc;
using UserGroup.Web.Models;

namespace UserGroup.Web.ViewComponents
{
    public class PersonSummaryViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke(PersonViewModel person)
        {
            return View(person);
        }
    }
}

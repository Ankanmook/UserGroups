using Microsoft.AspNetCore.Mvc;
using UserGroup.Common.Contracts;

namespace UserGroup.Web.ViewComponents
{
    public class PersonCountViewComponent : ViewComponent
    {
        private readonly IPersonService _personService;

        public PersonCountViewComponent(IPersonService personService)
        {
            _personService = personService;
        }

        public IViewComponentResult Invoke()
        {
            //best option is if this is cached in some kind of concurrent bag/collection
            return View(_personService.GetCount());
        }
    }
}
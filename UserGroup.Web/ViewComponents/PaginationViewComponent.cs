using Microsoft.AspNetCore.Mvc;
using UserGroup.Common.Contracts;
using UserGroup.Web.Models;

namespace UserGroup.Web.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        private readonly IPersonService _personService;

        public PaginationViewComponent(IPersonService personService)
        {
            _personService = personService;
        }

        public IViewComponentResult Invoke()
        {
            var paginationViewModel = new PaginationViewModel()
            {

            };
            
            return View(paginationViewModel);
        }
    }
}
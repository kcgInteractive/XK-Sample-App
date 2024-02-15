using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.Websites;
using Microsoft.AspNetCore.Mvc;

namespace Shared.ViewComponents;

public class FooterViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("Components/_Footer.cshtml");
    }
}

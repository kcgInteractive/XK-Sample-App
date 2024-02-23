using System.Threading.Tasks;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Photosnap.Widgets;

// [assembly: RegisterWidget(
//     identifier: "Audiophile.CTASection",
//     viewComponentType: typeof(CTASectionViewComponent),
//     name: "CTA Section",
//     IconClass = "icon-building-block"
// )]

namespace Audiophile.Widgets;

public class CTASectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Views/Photosnap/_CTASection.cshtml");
    }
}
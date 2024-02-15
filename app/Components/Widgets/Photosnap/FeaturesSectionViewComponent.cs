using System.Threading.Tasks;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Photosnap.Widgets;

[assembly: RegisterWidget(
    identifier: "Photosnap.FeaturesSection",
    viewComponentType: typeof(FeaturesSectionViewComponent),
    name: "Features Section",
    IconClass = "icon-building-block"
)]

namespace Photosnap.Widgets;

public class FeaturesSectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Views/Photosnap/_Features.cshtml");
    }
}

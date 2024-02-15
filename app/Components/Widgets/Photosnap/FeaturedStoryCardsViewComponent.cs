using System.Threading.Tasks;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Photosnap.Widgets;

[assembly: RegisterWidget(
    identifier: "Photosnap.FeaturedStoryCardsSection",
    viewComponentType: typeof(FeaturedStoryCardsViewComponent),
    name: "Featured Story Cards Section",
    IconClass = "icon-building-block"
)]

namespace Photosnap.Widgets;

public class FeaturedStoryCardsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Views/Photosnap/_FeaturedStoryCards.cshtml");
    }
}

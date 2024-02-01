using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models;

[assembly: RegisterWebPageRoute(
    contentTypeName: Shared.Page.CONTENT_TYPE_NAME,
    controllerType: typeof(PageController),
    WebsiteChannelNames = new[] { "Photosnap", "Audiophile" }
)]

namespace Shared.Controllers;

public class PageController : Controller
{
    private readonly IWebsiteChannelContext websiteChannelContext;

    public PageController(IWebsiteChannelContext websiteChannelContext)
    {
        this.websiteChannelContext = websiteChannelContext;
    }

    public IActionResult Index()
    {
        string ChannelContextName = websiteChannelContext.WebsiteChannelName;

        return View("~/Views/Shared/Page.cshtml", new PageViewModel(ChannelContextName));
    }
}

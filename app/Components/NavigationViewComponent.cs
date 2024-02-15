using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.Websites;
using Microsoft.AspNetCore.Mvc;

namespace Shared.ViewComponents;

public class NavigationViewComponent : ViewComponent
{
    private readonly IContentQueryExecutor executor;
    private readonly IWebPageQueryResultMapper mapper;

    public NavigationViewComponent(IContentQueryExecutor executor, IWebPageQueryResultMapper mapper)
    {
        this.executor = executor;
        this.mapper = mapper;
    }

    public async Task<IViewComponentResult> InvokeAsync(string channelName)
    {
        var headerQueryBuilder = new ContentItemQueryBuilder().ForContentType(
            contentTypeName: Page.CONTENT_TYPE_NAME,
            configureQuery: config => config.ForWebsite(websiteChannelName: channelName)
        );

        var queryOptions = new ContentQueryExecutionOptions() { ForPreview = true };

        IEnumerable<Page> pages = await executor.GetWebPageResult(
            builder: headerQueryBuilder,
            resultSelector: container => mapper.Map<Page>(container),
            options: queryOptions
        );

        return View("Components/_Navigation.cshtml", pages);
    }
}

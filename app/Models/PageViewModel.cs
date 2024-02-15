using Kentico.Content.Web.Mvc;

namespace Shared.Models;

public class PageViewModel
{
    public string ChannelName { get; init; }
    public string PageTitle { get; init; }

    public PageViewModel(string ChannelName, string PageTitle)
    {
        this.ChannelName = ChannelName;
        this.PageTitle = PageTitle;
    }
}

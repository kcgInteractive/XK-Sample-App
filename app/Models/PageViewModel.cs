using Kentico.Content.Web.Mvc;

namespace Shared.Models;

public class PageViewModel
{
    public string ChannelName { get; init; }
    public IMediaFileUrl FileUrl { get; init; }

    public string PageTitle { get; init; }

    public PageViewModel() { }
}

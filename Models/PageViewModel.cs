namespace Shared.Models;

public class PageViewModel
{
    public string ChannelContextName { get; init; }

    public PageViewModel(string ChannelContextName)
    {
        this.ChannelContextName = ChannelContextName;
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.Locations;

[assembly: UIPage(
    parentType: typeof(LocationsApplication),
    slug: "names",
    uiPageType: typeof(LocationsPage),
    name: "Names",
    templateName: "@sample/web-admin/Locations",
    order: 300
)]

namespace Site.Web.Admin.UIPages.Locations;

public class Location
{
    public string LocationGUID { get; set; }
    public string ChannelGUID { get; set; }
    public string CompanyLocationName { get; set; }
    public string Region { get; set; }
    public string CountryCode { get; set; }
    public string Country { get; set; }

#nullable enable
    public string? State { get; set; }

#nullable disable
    public string City { get; set; }
    public string Street { get; set; }

#nullable enable
    public string? Telephone { get; set; }
#nullable disable
}

public class Channel
{
    public string ChannelGUID { get; set; }
    public string ChannelDisplayName { get; set; }
}

public class PageTemplateClientProperties : TemplateClientProperties
{
    public IEnumerable<Channel> Channels { get; set; }
}

public class LocationsPage : Page<PageTemplateClientProperties>
{
    private readonly IInfoProvider<ChannelInfo> channelProvider;

    public LocationsPage(IInfoProvider<ChannelInfo> channelProvider)
    {
        this.channelProvider = channelProvider;
    }

    public override Task<PageTemplateClientProperties> ConfigureTemplateProperties(
        PageTemplateClientProperties properties
    )
    {
        IEnumerable<ChannelInfo> channelInfo = channelProvider.Get().GetEnumerableTypedResult();
        IEnumerable<Channel> channels = channelInfo.Select(
            (channel) =>
            {
                return new Channel
                {
                    ChannelGUID = channel.ChannelGUID.ToString(),
                    ChannelDisplayName = channel.ChannelDisplayName
                };
            }
        );

        properties.Channels = channels;
        return Task.FromResult(properties);
    }
}

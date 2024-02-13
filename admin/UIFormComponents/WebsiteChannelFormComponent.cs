using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using CMS.Membership;
using CMS.Websites;
using Kentico.Xperience.Admin.Base.Forms;

[assembly: RegisterFormComponent(
    "Custom.UIFormComponents.WebsiteChannelFormComponent",
    typeof(WebsiteChannelFormComponent),
    "Website Channel Selector"
)]

public class DropDownOption
{
    public string Value { get; set; }
    public string Text { get; set; }
}

public class MyComponentClientProperties : FormComponentClientProperties<string>
{
    public IEnumerable<DropDownOption> Options { get; set; }
}

public class WebsiteChannelFormComponent : FormComponent<MyComponentClientProperties, string>
{
    private readonly IInfoProvider<WebsiteChannelInfo> websiteChannelInfo;

    public WebsiteChannelFormComponent(IInfoProvider<WebsiteChannelInfo> websiteChannelInfo)
    {
        this.websiteChannelInfo = websiteChannelInfo;
    }

    public override string ClientComponentName => "@custom/web-admin/WebsiteChannel";

    protected override Task ConfigureClientProperties(MyComponentClientProperties clientProperties)
    {
        clientProperties.Options = websiteChannelInfo
            .Get()
            .GetEnumerableTypedResult()
            .Select(
                channelInfo =>
                    new DropDownOption()
                    {
                        Value = channelInfo.WebsiteChannelID.ToString(),
                        Text = channelInfo.WebsiteChannelDomain
                    }
            );

        return base.ConfigureClientProperties(clientProperties);
    }
}

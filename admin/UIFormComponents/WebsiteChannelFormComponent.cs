using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
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
    private readonly IInfoProvider<ChannelInfo> channelInfo;

    public WebsiteChannelFormComponent(IInfoProvider<ChannelInfo> channelInfo)
    {
        this.channelInfo = channelInfo;
    }

    public override string ClientComponentName => "@sample/web-admin/WebsiteChannel";

    protected override Task ConfigureClientProperties(MyComponentClientProperties clientProperties)
    {
        clientProperties.Options = channelInfo
            .Get()
            .GetEnumerableTypedResult()
            .Select(
                channelInfo =>
                    new DropDownOption()
                    {
                        Value = channelInfo.ChannelName,
                        Text = channelInfo.ChannelName
                    }
            );

        return base.ConfigureClientProperties(clientProperties);
    }
}

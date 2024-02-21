using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
using Kentico.Xperience.Admin.Base.Forms;

[assembly: RegisterFormComponent(
    "Custom.UIFormComponents.AssetSelectorFormComponent",
    typeof(AssetSelectorFormComponent),
    "Asset Selector"
)]

public class AssetSelectorFormComponentClientProperties : FormComponentClientProperties<string> { }

public class AssetSelectorFormComponent
    : FormComponent<AssetSelectorFormComponentClientProperties, string>
{
    private readonly IInfoProvider<ChannelInfo> channelInfo;

    public override string ClientComponentName => "@site/web-admin/AssetSelector";
}

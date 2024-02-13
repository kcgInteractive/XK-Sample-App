using System.Threading.Tasks;
using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.SiteSettings;
using SiteSettingsModule;

[assembly: UIPage(
    parentType: typeof(SiteSettingsApplication),
    slug: "list",
    uiPageType: typeof(SiteSettingsListing),
    name: "Site setting list",
    templateName: TemplateNames.LISTING,
    order: UIPageOrder.First
)]

namespace Site.Web.Admin.UIPages.SiteSettings;

public class SiteSettingsListing : ListingPage
{
    protected override string ObjectType => SiteSettingsInfo.OBJECT_TYPE;

    [PageCommand]
    public override Task<ICommandResponse<RowActionResult>> Delete(int id) => base.Delete(id);

    public override Task ConfigurePage()
    {
        PageConfiguration.HeaderActions.AddLink<SiteSettingsCreate>("New site setting");
        PageConfiguration.ColumnConfigurations.AddColumn(
            nameof(SiteSettingsInfo.ChannelName),
            "Site name"
        );

        PageConfiguration.AddEditRowAction<SiteSettingsEdit>();
        PageConfiguration.TableActions.AddDeleteAction("Delete");

        return base.ConfigurePage();
    }
}

using System.Threading.Tasks;
using Acme.Web.Admin.UIPages.OfficeManagement;
using AcmeModule;
using Kentico.Xperience.Admin.Base;

// [assembly: UIPage(
//     parentType: typeof(OfficeManagementApplication),
//     slug: "list",
//     uiPageType: typeof(OfficeListing),
//     name: "Offices",
//     templateName: TemplateNames.LISTING,
//     order: UIPageOrder.First
// )]

namespace Acme.Web.Admin.UIPages.OfficeManagement;

public class OfficeListing : ListingPage
{
    protected override string ObjectType => OfficeInfo.OBJECT_TYPE;

    public override Task ConfigurePage()
    {
        PageConfiguration.HeaderActions.AddLink<OfficeCreate>("New office");
        PageConfiguration.ColumnConfigurations.AddColumn(
            nameof(OfficeInfo.OfficeDisplayName),
            "Office name"
        );

        PageConfiguration.AddEditRowAction<OfficeEdit>();

        return base.ConfigurePage();
    }
}

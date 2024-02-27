using Kentico.Xperience.Admin.Base;
using Site;
using Site.Web.Admin.UIPages.Locations;

[assembly: UIApplication(
    identifier: LocationsApplication.IDENTIFIER,
    type: typeof(LocationsApplication),
    slug: "locations",
    name: "Locations",
    category: SiteAdminModule.CUSTOM_CATEGORY,
    icon: Icons.MapMarker,
    templateName: TemplateNames.SECTION_LAYOUT
)]

namespace Site.Web.Admin.UIPages.Locations;

public class LocationsApplication : ApplicationPage
{
    public const string IDENTIFIER = "Site.Web.Application.Locations";
}

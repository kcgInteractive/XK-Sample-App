using Kentico.Xperience.Admin.Base;
using Site;
using Site.Web.Admin.UIPages.Taxonomies;

[assembly: UIApplication(
    identifier: TaxonomiesApplication.IDENTIFIER,
    type: typeof(TaxonomiesApplication),
    slug: "taxonomy",
    name: "Taxonomy",
    category: SiteAdminModule.CUSTOM_CATEGORY,
    icon: Icons.ProjectScheme,
    templateName: TemplateNames.SECTION_LAYOUT
)]

namespace Site.Web.Admin.UIPages.Taxonomies;

public class TaxonomiesApplication : ApplicationPage
{
    public const string IDENTIFIER = "Site.Web.Application.Taxonomies";
}

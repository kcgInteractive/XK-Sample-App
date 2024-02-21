using System.Threading.Tasks;
using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.Taxonomies;

// Defines an admin UI page

[assembly: UIPage(
    parentType: typeof(TaxonomiesApplication),
    slug: "names",
    uiPageType: typeof(TaxonomiesPage),
    name: "Taxonomy names",
    templateName: "@sample/web-admin/Taxonomies",
    order: 300
)]

namespace Site.Web.Admin.UIPages.Taxonomies;

public class TaxonomiesPage : Page { }

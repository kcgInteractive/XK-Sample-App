// Registers a new category for custom admin UI applications
using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.CustomApplications;

[assembly: UICategory(
    codeName: CustomApplications.CUSTOM_CATEGORY,
    name: "Custom applications",
    icon: Icons.CustomElement,
    order: 400
)]

namespace Site.Web.Admin.UIPages.CustomApplications;

public class CustomApplications : ApplicationPage
{
    public const string IDENTIFIER = "Site.Web.Application.CustomApplication";
    public const string CUSTOM_CATEGORY = "site.web.admin.category";
}

using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.SiteSettings;

// Registers a new category for custom admin UI applications
[assembly: UICategory(
    codeName: SiteSettingsApplication.CUSTOM_CATEGORY,
    name: "Custom applications",
    icon: Icons.CustomElement,
    order: 400
)]

[assembly: UIApplication(
    identifier: SiteSettingsApplication.IDENTIFIER,
    type: typeof(SiteSettingsApplication),
    slug: "site-settings",
    name: "Site settings",
    category: SiteSettingsApplication.CUSTOM_CATEGORY,
    icon: Icons.Cogwheel,
    templateName: TemplateNames.SECTION_LAYOUT
)]

namespace Site.Web.Admin.UIPages.SiteSettings;

public class SiteSettingsApplication : ApplicationPage
{
    public const string IDENTIFIER = "Site.Web.Application.SiteSettings";
    public const string CUSTOM_CATEGORY = "site.web.admin.category";
}

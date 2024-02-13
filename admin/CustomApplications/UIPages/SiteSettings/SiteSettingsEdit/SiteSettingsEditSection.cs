using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.SiteSettings;
using SiteSettingsModule;

[assembly: UIPage(
    parentType: typeof(SiteSettingsListing),
    slug: PageParameterConstants.PARAMETERIZED_SLUG,
    uiPageType: typeof(SiteSettingsEditSection),
    name: "Edit section for site setting objects",
    templateName: TemplateNames.SECTION_LAYOUT,
    order: 300
)]

namespace Site.Web.Admin.UIPages.SiteSettings;

public class SiteSettingsEditSection : EditSectionPage<SiteSettingsInfo> { }

using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;
using Site.Web.Admin.UIPages.SiteSettings;
using SiteSettingsModule;

[assembly: UIPage(
    parentType: typeof(SiteSettingsListing),
    slug: "create",
    uiPageType: typeof(SiteSettingsCreate),
    name: "Create site setting",
    templateName: TemplateNames.EDIT,
    order: 200
)]

namespace Site.Web.Admin.UIPages.SiteSettings;

public class SiteSettingsCreate : CreatePage<SiteSettingsInfo, SiteSettingsEditSection>
{
    public SiteSettingsCreate(
        IFormComponentMapper mapper,
        IFormDataBinder binder,
        IPageUrlGenerator generator
    )
        : base(mapper, binder, generator) { }
}

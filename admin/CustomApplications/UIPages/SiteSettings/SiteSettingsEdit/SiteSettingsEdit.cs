using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;
using Site.Web.Admin.UIPages.SiteSettings;
using SiteSettingsModule;

[assembly: UIPage(
    parentType: typeof(SiteSettingsEditSection),
    slug: "edit",
    uiPageType: typeof(SiteSettingsEdit),
    name: "Edit site setting",
    templateName: TemplateNames.EDIT,
    order: UIPageOrder.First
)]

namespace Site.Web.Admin.UIPages.SiteSettings;

public class SiteSettingsEdit : InfoEditPage<SiteSettingsInfo>
{
    public SiteSettingsEdit(
        IFormComponentMapper formComponentMapper,
        IFormDataBinder formDataBinder
    )
        : base(formComponentMapper, formDataBinder) { }

    [PageParameter(typeof(IntPageModelBinder))]
    public override int ObjectId { get; set; }
}

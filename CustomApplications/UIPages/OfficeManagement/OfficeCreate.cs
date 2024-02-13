using Acme.Web.Admin.UIPages.OfficeManagement;
using AcmeModule;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;

// [assembly: UIPage(
//     parentType: typeof(OfficeListing),
//     slug: "create",
//     uiPageType: typeof(OfficeCreate),
//     name: "OfficeCreate",
//     templateName: TemplateNames.EDIT,
//     order: 200
// )]

namespace Acme.Web.Admin.UIPages.OfficeManagement;

public class OfficeCreate : CreatePage<OfficeInfo, OfficeEditSection>
{
    public OfficeCreate(
        IFormComponentMapper mapper,
        IFormDataBinder binder,
        IPageUrlGenerator generator
    )
        : base(mapper, binder, generator) { }
}

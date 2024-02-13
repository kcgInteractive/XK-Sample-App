using Acme.Web.Admin.UIPages.OfficeManagement;
using AcmeModule;
using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.Forms;

// [assembly: UIPage(
//     parentType: typeof(OfficeEditSection),
//     slug: "edit",
//     uiPageType: typeof(OfficeEdit),
//     name: "Edit an office",
//     templateName: TemplateNames.EDIT,
//     order: UIPageOrder.First
// )]

namespace Acme.Web.Admin.UIPages.OfficeManagement;

public class OfficeEdit : InfoEditPage<OfficeInfo>
{
    public OfficeEdit(IFormComponentMapper formComponentMapper, IFormDataBinder formDataBinder)
        : base(formComponentMapper, formDataBinder) { }

    [PageParameter(typeof(IntPageModelBinder))]
    public override int ObjectId { get; set; }
}

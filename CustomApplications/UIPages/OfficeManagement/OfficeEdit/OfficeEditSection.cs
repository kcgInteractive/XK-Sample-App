using Acme.Web.Admin.UIPages.OfficeManagement;
using AcmeModule;
using Kentico.Xperience.Admin.Base;

// [assembly: UIPage(
//     parentType: typeof(OfficeListing),
//     slug: PageParameterConstants.PARAMETERIZED_SLUG,
//     uiPageType: typeof(OfficeEditSection),
//     name: "Edit section for office objects",
//     templateName: TemplateNames.SECTION_LAYOUT,
//     order: 300
// )]

namespace Acme.Web.Admin.UIPages.OfficeManagement;

public class OfficeEditSection : EditSectionPage<OfficeInfo> { }

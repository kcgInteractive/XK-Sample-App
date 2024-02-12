using Acme.Web.Admin.UIPages.OfficeManagement;
using Kentico.Xperience.Admin.Base;

// Registers a new category for custom admin UI applications
// [assembly: UICategory(
//     codeName: OfficeManagementApplication.CUSTOM_CATEGORY,
//     name: "Custom applications",
//     icon: Icons.CustomElement,
//     order: 400
// )]

// [assembly: UIApplication(
//     identifier: OfficeManagementApplication.IDENTIFIER,
//     type: typeof(OfficeManagementApplication),
//     slug: "officeManagement",
//     name: "Office management",
//     category: OfficeManagementApplication.CUSTOM_CATEGORY,
//     icon: Icons.Apple,
//     templateName: TemplateNames.SECTION_LAYOUT
// )]

namespace Acme.Web.Admin.UIPages.OfficeManagement;

public class OfficeManagementApplication : ApplicationPage
{
    public const string IDENTIFIER = "Acme.Web.Application.OfficeManagement";
    public const string CUSTOM_CATEGORY = "acme.web.admin.category";
}

using Kentico.Xperience.Admin.Base;
using Site;

[assembly: CMS.AssemblyDiscoverable]
[assembly: CMS.RegisterModule(typeof(SiteAdminModule))]

// Adds a new application category
[assembly: UICategory(SiteAdminModule.CUSTOM_CATEGORY, "Custom", Icons.CustomElement, 100)]

namespace Site;

public class SiteAdminModule : AdminModule
{
    public const string CUSTOM_CATEGORY = "site.web.admin.category";

    // (Optional) Change the name of the custom module
    public SiteAdminModule()
        : base("Sample.Web.Admin") { }

    protected override void OnInit()
    {
        base.OnInit();

        // Change the organization name and project name in the client scripts registration
        RegisterClientModule("sample", "web-admin");
    }
}

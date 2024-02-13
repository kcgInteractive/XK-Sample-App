using AdminCustomization;
using Kentico.Xperience.Admin.Base;

[assembly: CMS.AssemblyDiscoverable]
[assembly: CMS.RegisterModule(typeof(SampleWebAdminModule))]

// Adds a new application category
[assembly: UICategory(SampleWebAdminModule.CUSTOM_CATEGORY, "Custom", Icons.CustomElement, 100)]

namespace AdminCustomization
{
    internal class SampleWebAdminModule : AdminModule
    {
        public const string CUSTOM_CATEGORY = "sample.web.admin.category";

        public SampleWebAdminModule()
            : base("Sample.Web.Admin") { }

        protected override void OnInit()
        {
            base.OnInit();

            // Makes the module accessible to the admin UI
            RegisterClientModule("sample", "web-admin");
        }
    }
}

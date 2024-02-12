using CMS.DataEngine;

namespace SiteSettingsModule
{
    /// <summary>
    /// Class providing <see cref="SiteSettingsInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ISiteSettingsInfoProvider))]
    public partial class SiteSettingsInfoProvider : AbstractInfoProvider<SiteSettingsInfo, SiteSettingsInfoProvider>, ISiteSettingsInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteSettingsInfoProvider"/> class.
        /// </summary>
        public SiteSettingsInfoProvider()
            : base(SiteSettingsInfo.TYPEINFO)
        {
        }
    }
}
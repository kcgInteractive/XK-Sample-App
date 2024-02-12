using CMS.DataEngine;

namespace SiteSettingsModule
{
    /// <summary>
    /// Declares members for <see cref="SiteSettingsInfo"/> management.
    /// </summary>
    public partial interface ISiteSettingsInfoProvider : IInfoProvider<SiteSettingsInfo>, IInfoByIdProvider<SiteSettingsInfo>, IInfoByNameProvider<SiteSettingsInfo>
    {
    }
}
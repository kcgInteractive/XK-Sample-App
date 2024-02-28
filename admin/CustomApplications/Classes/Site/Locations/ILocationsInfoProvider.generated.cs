using CMS.DataEngine;

namespace Site.Location
{
    /// <summary>
    /// Declares members for <see cref="LocationsInfo"/> management.
    /// </summary>
    public partial interface ILocationsInfoProvider : IInfoProvider<LocationsInfo>, IInfoByIdProvider<LocationsInfo>, IInfoByGuidProvider<LocationsInfo>
    {
    }
}
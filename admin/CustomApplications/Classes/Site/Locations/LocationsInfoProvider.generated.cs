using CMS.DataEngine;

namespace Site.Location
{
    /// <summary>
    /// Class providing <see cref="LocationsInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ILocationsInfoProvider))]
    public partial class LocationsInfoProvider : AbstractInfoProvider<LocationsInfo, LocationsInfoProvider>, ILocationsInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationsInfoProvider"/> class.
        /// </summary>
        public LocationsInfoProvider()
            : base(LocationsInfo.TYPEINFO)
        {
        }
    }
}
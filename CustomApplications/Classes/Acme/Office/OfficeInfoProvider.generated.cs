using CMS.DataEngine;

namespace AcmeModule
{
    /// <summary>
    /// Class providing <see cref="OfficeInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(IOfficeInfoProvider))]
    public partial class OfficeInfoProvider : AbstractInfoProvider<OfficeInfo, OfficeInfoProvider>, IOfficeInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OfficeInfoProvider"/> class.
        /// </summary>
        public OfficeInfoProvider()
            : base(OfficeInfo.TYPEINFO)
        {
        }
    }
}
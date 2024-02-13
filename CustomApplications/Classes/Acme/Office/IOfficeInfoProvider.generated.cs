using CMS.DataEngine;

namespace AcmeModule
{
    /// <summary>
    /// Declares members for <see cref="OfficeInfo"/> management.
    /// </summary>
    public partial interface IOfficeInfoProvider : IInfoProvider<OfficeInfo>, IInfoByIdProvider<OfficeInfo>
    {
    }
}
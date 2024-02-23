using CMS.DataEngine;

namespace Taxonomies
{
    /// <summary>
    /// Declares members for <see cref="TaxonomyInfo"/> management.
    /// </summary>
    public partial interface ITaxonomyInfoProvider : IInfoProvider<TaxonomyInfo>, IInfoByIdProvider<TaxonomyInfo>
    {
    }
}
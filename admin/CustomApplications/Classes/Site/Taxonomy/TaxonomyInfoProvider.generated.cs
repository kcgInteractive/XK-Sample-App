using CMS.DataEngine;

namespace Taxonomies
{
    /// <summary>
    /// Class providing <see cref="TaxonomyInfo"/> management.
    /// </summary>
    [ProviderInterface(typeof(ITaxonomyInfoProvider))]
    public partial class TaxonomyInfoProvider : AbstractInfoProvider<TaxonomyInfo, TaxonomyInfoProvider>, ITaxonomyInfoProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonomyInfoProvider"/> class.
        /// </summary>
        public TaxonomyInfoProvider()
            : base(TaxonomyInfo.TYPEINFO)
        {
        }
    }
}
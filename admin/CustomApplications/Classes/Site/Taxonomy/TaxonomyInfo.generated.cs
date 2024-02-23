using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Taxonomies;

[assembly: RegisterObjectType(typeof(TaxonomyInfo), TaxonomyInfo.OBJECT_TYPE)]

namespace Taxonomies
{
    /// <summary>
    /// Data container class for <see cref="TaxonomyInfo"/>.
    /// </summary>
    [Serializable]
    public partial class TaxonomyInfo : AbstractInfo<TaxonomyInfo, ITaxonomyInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "site.taxonomy";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(TaxonomyInfoProvider), OBJECT_TYPE, "Site.Taxonomy", "TaxonomyID", null, null, null, "DisplayName", null, null, null)
        {
            TouchCacheDependencies = true,
        };


        /// <summary>
        /// Taxonomy ID.
        /// </summary>
        [DatabaseField]
        public virtual int TaxonomyID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(TaxonomyID)), 0);
            set => SetValue(nameof(TaxonomyID), value);
        }


        /// <summary>
        /// Parent ID.
        /// </summary>
        [DatabaseField]
        public virtual int ParentID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(ParentID)), 0);
            set => SetValue(nameof(ParentID), value);
        }


        /// <summary>
        /// Parent value.
        /// </summary>
        [DatabaseField]
        public virtual string ParentValue
        {
            get => ValidationHelper.GetString(GetValue(nameof(ParentValue)), String.Empty);
            set => SetValue(nameof(ParentValue), value);
        }


        /// <summary>
        /// Display name.
        /// </summary>
        [DatabaseField]
        public virtual string DisplayName
        {
            get => ValidationHelper.GetString(GetValue(nameof(DisplayName)), String.Empty);
            set => SetValue(nameof(DisplayName), value);
        }


        /// <summary>
        /// Value.
        /// </summary>
        [DatabaseField]
        public virtual string Value
        {
            get => ValidationHelper.GetString(GetValue(nameof(Value)), String.Empty);
            set => SetValue(nameof(Value), value);
        }


        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            Provider.Delete(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            Provider.Set(this);
        }


        /// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected TaxonomyInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="TaxonomyInfo"/> class.
        /// </summary>
        public TaxonomyInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="TaxonomyInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public TaxonomyInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}
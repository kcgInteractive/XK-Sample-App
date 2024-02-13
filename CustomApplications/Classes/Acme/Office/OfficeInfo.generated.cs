using System;
using System.Data;
using System.Runtime.Serialization;
using System.Collections.Generic;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using AcmeModule;

[assembly: RegisterObjectType(typeof(OfficeInfo), OfficeInfo.OBJECT_TYPE)]

namespace AcmeModule
{
    /// <summary>
    /// Data container class for <see cref="OfficeInfo"/>.
    /// </summary>
    [Serializable]
    public partial class OfficeInfo : AbstractInfo<OfficeInfo, IOfficeInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "acme.office";


        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(OfficeInfoProvider), OBJECT_TYPE, "Acme.Office", "OfficeID", null, null, null, "OfficeDisplayName", null, null, null)
        {
            TouchCacheDependencies = true,
            DependsOn = new List<ObjectDependency>()
            {
                new ObjectDependency("OfficeType", "cms.websitechannel", ObjectDependencyEnum.Required),
            },
        };


        /// <summary>
        /// Office ID.
        /// </summary>
        [DatabaseField]
        public virtual int OfficeID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(OfficeID)), 0);
            set => SetValue(nameof(OfficeID), value);
        }


        /// <summary>
        /// Office type.
        /// </summary>
        [DatabaseField]
        public virtual int OfficeType
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(OfficeType)), 0);
            set => SetValue(nameof(OfficeType), value, 0);
        }


        /// <summary>
        /// Office display name.
        /// </summary>
        [DatabaseField]
        public virtual string OfficeDisplayName
        {
            get => ValidationHelper.GetString(GetValue(nameof(OfficeDisplayName)), String.Empty);
            set => SetValue(nameof(OfficeDisplayName), value);
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
        protected OfficeInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }


        /// <summary>
        /// Creates an empty instance of the <see cref="OfficeInfo"/> class.
        /// </summary>
        public OfficeInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Creates a new instances of the <see cref="OfficeInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public OfficeInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}
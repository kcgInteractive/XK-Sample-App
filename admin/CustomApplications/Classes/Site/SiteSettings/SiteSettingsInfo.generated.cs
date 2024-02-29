using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using SiteSettingsModule;

[assembly: RegisterObjectType(typeof(SiteSettingsInfo), SiteSettingsInfo.OBJECT_TYPE)]

namespace SiteSettingsModule
{
    /// <summary>
    /// Data container class for <see cref="SiteSettingsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class SiteSettingsInfo : AbstractInfo<SiteSettingsInfo, ISiteSettingsInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "site.sitesettings";

        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(SiteSettingsInfoProvider), OBJECT_TYPE, "Site.SiteSettings", "SettingsID", null, "GUID", null, null, null, null, null)
        {
            TouchCacheDependencies = true,
             ContinuousIntegrationSettings = {Enabled = true}
        };

        /// <summary>
        /// Settings ID.
        /// </summary>
        [DatabaseField]
        public virtual int SettingsID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(SettingsID)), 0);
            set => SetValue(nameof(SettingsID), value);
        }

        /// <summary>
        /// Channel name.
        /// </summary>
        [DatabaseField]
        public virtual string ChannelName
        {
            get => ValidationHelper.GetString(GetValue(nameof(ChannelName)), String.Empty);
            set => SetValue(nameof(ChannelName), value);
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
        protected SiteSettingsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="SiteSettingsInfo"/> class.
        /// </summary>
        public SiteSettingsInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="SiteSettingsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public SiteSettingsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}
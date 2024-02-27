using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;
using Site.Location;

[assembly: RegisterObjectType(typeof(LocationsInfo), LocationsInfo.OBJECT_TYPE)]

namespace Site.Location
{
    /// <summary>
    /// Data container class for <see cref="LocationsInfo"/>.
    /// </summary>
    [Serializable]
    public partial class LocationsInfo : AbstractInfo<LocationsInfo, ILocationsInfoProvider>
    {
        /// <summary>
        /// Object type.
        /// </summary>
        public const string OBJECT_TYPE = "site.locations";

        /// <summary>
        /// Type information.
        /// </summary>
#warning "You will need to configure the type info."
        public static readonly ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(LocationsInfoProvider), OBJECT_TYPE, "Site.Locations", "LocationsID", null, "LocationGUID", "CompanyLocationName", null, null, null, null)
        {
            TouchCacheDependencies = true,
            ContinuousIntegrationSettings = {Enabled = true}
        };

        /// <summary>
        /// Locations ID.
        /// </summary>
        [DatabaseField]
        public virtual int LocationsID
        {
            get => ValidationHelper.GetInteger(GetValue(nameof(LocationsID)), 0);
            set => SetValue(nameof(LocationsID), value);
        }

        /// <summary>
        /// Channel GUID.
        /// </summary>
        [DatabaseField]
        public virtual Guid ChannelGUID
        {
            get => ValidationHelper.GetGuid(GetValue(nameof(ChannelGUID)), Guid.Empty);
            set => SetValue(nameof(ChannelGUID), value);
        }

        /// <summary>
        /// Location GUID.
        /// </summary>
        [DatabaseField]
        public virtual Guid LocationGUID
        {
            get => ValidationHelper.GetGuid(GetValue(nameof(LocationGUID)), Guid.Empty);
            set => SetValue(nameof(LocationGUID), value);
        }

        /// <summary>
        /// Company location name.
        /// </summary>
        [DatabaseField]
        public virtual string CompanyLocationName
        {
            get => ValidationHelper.GetString(GetValue(nameof(CompanyLocationName)), String.Empty);
            set => SetValue(nameof(CompanyLocationName), value);
        }

        /// <summary>
        /// Region.
        /// </summary>
        [DatabaseField]
        public virtual string Region
        {
            get => ValidationHelper.GetString(GetValue(nameof(Region)), String.Empty);
            set => SetValue(nameof(Region), value);
        }

        /// <summary>
        /// Country.
        /// </summary>
        [DatabaseField]
        public virtual string Country
        {
            get => ValidationHelper.GetString(GetValue(nameof(Country)), String.Empty);
            set => SetValue(nameof(Country), value);
        }

        /// <summary>
        /// State.
        /// </summary>
        [DatabaseField]
        public virtual string State
        {
            get => ValidationHelper.GetString(GetValue(nameof(State)), String.Empty);
            set => SetValue(nameof(State), value, String.Empty);
        }

        /// <summary>
        /// City.
        /// </summary>
        [DatabaseField]
        public virtual string City
        {
            get => ValidationHelper.GetString(GetValue(nameof(City)), String.Empty);
            set => SetValue(nameof(City), value);
        }

        /// <summary>
        /// Street.
        /// </summary>
        [DatabaseField]
        public virtual string Street
        {
            get => ValidationHelper.GetString(GetValue(nameof(Street)), String.Empty);
            set => SetValue(nameof(Street), value);
        }

        /// <summary>
        /// Phone.
        /// </summary>
        [DatabaseField]
        public virtual string Phone
        {
            get => ValidationHelper.GetString(GetValue(nameof(Phone)), String.Empty);
            set => SetValue(nameof(Phone), value, String.Empty);
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
        protected LocationsInfo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="LocationsInfo"/> class.
        /// </summary>
        public LocationsInfo()
            : base(TYPEINFO)
        {
        }

        /// <summary>
        /// Creates a new instances of the <see cref="LocationsInfo"/> class from the given <see cref="DataRow"/>.
        /// </summary>
        /// <param name="dr">DataRow with the object data.</param>
        public LocationsInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using CMS.ContentEngine;
using CMS.MediaLibrary;

namespace Site
{
    /// <summary>
    /// Represents a content item of type <see cref="SiteSettings"/>.
    /// </summary>
    public partial class SiteSettings
    {
        /// <summary>
        /// Code name of the content type.
        /// </summary>
        public const string CONTENT_TYPE_NAME = "Site.SiteSettings";

        /// <summary>
        /// Text.
        /// </summary>
        public string SiteName { get; set; }

        public IEnumerable<AssetRelatedItem> SiteLogo { get; set; }
    }
}

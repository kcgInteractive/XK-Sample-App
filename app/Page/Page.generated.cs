//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS.Websites;
using CMS.MediaLibrary;

namespace Shared
{
	/// <summary>
	/// Represents a page of type <see cref="Page"/>.
	/// </summary>
	public partial class Page : IWebPageFieldsSource
	{
		/// <summary>
		/// Code name of the content type.
		/// </summary>
		public const string CONTENT_TYPE_NAME = "Shared.Page";


		/// <summary>
		/// Represents system properties for a web page item.
		/// </summary>
		public WebPageFields SystemFields { get; set; }


		/// <summary>
		/// IncludeNav.
		/// </summary>
		public bool IncludeNav { get; set; }


		/// <summary>
		/// Media.
		/// </summary>
		public IEnumerable<AssetRelatedItem> Media { get; set; }
	}
}
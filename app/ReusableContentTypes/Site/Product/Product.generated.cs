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
using CMS.ContentEngine;

namespace Site
{
	/// <summary>
	/// Represents a content item of type <see cref="Product"/>.
	/// </summary>
	public partial class Product
	{
		/// <summary>
		/// Code name of the content type.
		/// </summary>
		public const string CONTENT_TYPE_NAME = "Site.Product";


		/// <summary>
		/// Represents system properties for a content item.
		/// </summary>
		public ContentItemFields SystemFields { get; set; }


		/// <summary>
		/// ProductName.
		/// </summary>
		public string ProductName { get; set; }


		/// <summary>
		/// Short_Description.
		/// </summary>
		public string Short_Description { get; set; }
	}
}
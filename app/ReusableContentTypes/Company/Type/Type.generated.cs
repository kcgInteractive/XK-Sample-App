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

namespace Company
{
	/// <summary>
	/// Represents a content item of type <see cref="Type"/>.
	/// </summary>
	public partial class Type
	{
		/// <summary>
		/// Code name of the content type.
		/// </summary>
		public const string CONTENT_TYPE_NAME = "Company.Type";


		/// <summary>
		/// Represents system properties for a content item.
		/// </summary>
		public ContentItemFields SystemFields { get; set; }


		/// <summary>
		/// CompanyID.
		/// </summary>
		public int CompanyID { get; set; }


		/// <summary>
		/// CompanyName.
		/// </summary>
		public string CompanyName { get; set; }


		/// <summary>
		/// Description.
		/// </summary>
		public string Description { get; set; }


		/// <summary>
		/// Tags.
		/// </summary>
		public string Tags { get; set; }
	}
}
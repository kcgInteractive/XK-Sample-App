// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using CMS.ContentEngine;
// using CMS.DataEngine;
// using Kentico.Xperience.Admin.Base.Forms;
// using Taxonomies;

// [assembly: RegisterFormComponent(
//     "Custom.UIFormComponents.ListCreatorFormComponent",
//     typeof(ListCreatorFormComponent),
//     "Taxonomy Selector"
// )]

// public class Taxonomy
// {
//     public int CategoryID { get; set; }
//     public string Name { get; set; }
// }

// public class TaxonomyCategory
// {
//     public int ID { get; set; }
//     public string Name { get; set; }
// }

// public class ListCreatorComponentClientProperties : FormComponentClientProperties<string>
// {
//     public IEnumerable<TaxonomyCategory> Categories { get; set; }
//     public IEnumerable<Taxonomy> Taxonomies { get; set; }
// }

// public class ListCreatorFormComponent : FormComponent<ListCreatorComponentClientProperties, string>
// {
//     private readonly IInfoProvider<CategoryInfo> categoryInfo;
//     private readonly IInfoProvider<NamesInfo> namesInfo;

//     public ListCreatorFormComponent(
//         IInfoProvider<CategoryInfo> categoryInfo,
//         IInfoProvider<NamesInfo> namesInfo
//     )
//     {

//         this.categoryInfo = categoryInfo;
//         this.namesInfo = namesInfo;
//     }

//     public override string ClientComponentName => "@sample/web-admin/ListCreator";

//     protected override Task ConfigureClientProperties(
//         ListCreatorComponentClientProperties clientProperties
//     )
//     {
//         clientProperties.Categories = categoryInfo
//             .Get()
//             .GetEnumerableTypedResult()
//             .Select(
//                 categoryInfo =>
//                     new TaxonomyCategory()
//                     {
//                         ID = categoryInfo.CategoryID,
//                         Name = categoryInfo.Category
//                     }
//             );

//         clientProperties.Taxonomies = namesInfo
//             .Get()
//             .GetEnumerableTypedResult()
//             .Select(
//                 namesInfo =>
//                     new Taxonomy()
//                     {
//                         CategoryID = namesInfo.CategoryID,
//                         Name = namesInfo.TaxonomyName
//                     }
//             );

//         return base.ConfigureClientProperties(clientProperties);
//     }
// }

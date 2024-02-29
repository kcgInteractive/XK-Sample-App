// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Algolia.Search.Models.Settings;
// using CMS.ContentEngine;
// using Kentico.Xperience.Algolia.Indexing;
// using Kentico.Xperience.Algolia.Search;
// using Newtonsoft.Json.Linq;
// using Site;

// public class ProductSearchIndexingStrategy : DefaultAlgoliaIndexingStrategy
// {
//     private readonly IContentQueryResultMapper contentMapper;
//     private readonly IContentQueryExecutor queryExecutor;

//     public ProductSearchIndexingStrategy(
//         IContentQueryResultMapper contentMapper,
//         IContentQueryExecutor queryExecutor
//     )
//     {
//         this.contentMapper = contentMapper;
//         this.queryExecutor = queryExecutor;
//     }

//     public static string SORTABLE_TITLE_FIELD_NAME = "SortableTitle";

//     public override IndexSettings GetAlgoliaIndexSettings() =>
//         new()
//         {
//             AttributesToRetrieve = new List<string> { nameof(ProductSearchResultModel.Title) }
//         };

//     public override async Task<IEnumerable<JObject>> MapToAlgoliaJObjectsOrNull(
//         IIndexEventItemModel algoliaContentItem
//     )
//     {
//         var resultProperties = new ProductSearchResultModel();

//         var result = new List<JObject>();

//         string title = "";

//         if (algoliaContentItem is IndexEventReusableItemModel indexedContentItem)
//         {
//             if (
//                 string.Equals(
//                     algoliaContentItem.ContentTypeName,
//                     Product.CONTENT_TYPE_NAME,
//                     System.StringComparison.OrdinalIgnoreCase
//                 )
//             )
//             {
//                 var query = new ContentItemQueryBuilder().ForContentType(Product.CONTENT_TYPE_NAME);

//                 var queryResult = await queryExecutor.GetResult(query, contentMapper.Map<Product>);

//                 title = queryResult.FirstOrDefault()!.ProductName;
//             }
//         }

//         var jObject = new JObject();

//         jObject[nameof(ProductSearchResultModel.Title)] = title;

//         result.Add(jObject);
//         return result;
//     }

//     private JObject AssignProperties<T>(T value)
//         where T : AlgoliaSearchResultModel
//     {
//         var jObject = new JObject();

//         foreach (var prop in value.GetType().GetProperties())
//         {
//             var type = prop.PropertyType;
//             if (type == typeof(string))
//             {
//                 jObject[prop.Name] = (string)prop.GetValue(value);
//             }
//             else if (type == typeof(int))
//             {
//                 jObject[prop.Name] = (int)prop.GetValue(value);
//             }
//             else if (type == typeof(bool))
//             {
//                 jObject[prop.Name] = (bool)prop.GetValue(value);
//             }
//         }

//         return jObject;
//     }
// }

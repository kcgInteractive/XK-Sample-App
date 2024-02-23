using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using CMS.DataEngine.Internal;
using Kentico.Xperience.Admin.Base;
using Site.Web.Admin.UIPages.Taxonomies;
using Taxonomies;

// Defines an admin UI page

[assembly: UIPage(
    parentType: typeof(TaxonomiesApplication),
    slug: "categories",
    uiPageType: typeof(TaxonomiesPage),
    name: "Categories",
    templateName: "@sample/web-admin/Taxonomies",
    order: 300
)]

namespace Site.Web.Admin.UIPages.Taxonomies;

public class CommandResult
{
    public IEnumerable<TaxonomyCategory> Taxonomies { get; set; }

    public CommandResult(IEnumerable<TaxonomyCategory> Taxonomies)
    {
        this.Taxonomies = Taxonomies;
    }
}

public class TaxonomyCategory
{
    public int ID { get; set; }
    public int ParentID { get; set; }
    public string Value { get; set; }
    public string DisplayName { get; set; }
    public string ParentValue { get; set; }
    public string Description { get; set; }
}

public class PageTemplateClientProperties : TemplateClientProperties
{
    public IEnumerable<TaxonomyCategory> InitialTaxonomies { get; set; }
}

public class TaxonomiesPage : Page<PageTemplateClientProperties>
{
    private readonly IInfoProvider<TaxonomyInfo> taxonomyProvider;

    public TaxonomiesPage(IInfoProvider<TaxonomyInfo> taxonomyProvider)
    {
        this.taxonomyProvider = taxonomyProvider;
    }

    public override Task<PageTemplateClientProperties> ConfigureTemplateProperties(
        PageTemplateClientProperties properties
    )
    {
        IEnumerable<TaxonomyInfo> taxonomyInfo = taxonomyProvider.Get().GetEnumerableTypedResult();

        IEnumerable<TaxonomyCategory> taxonomiesData = taxonomyInfo.Select(
            (taxonomy) =>
            {
                return new TaxonomyCategory
                {
                    ID = taxonomy.TaxonomyID,
                    ParentID = taxonomy.ParentID,
                    DisplayName = taxonomy.DisplayName,
                    Value = taxonomy.Value,
                    ParentValue = taxonomy.ParentValue,
                    Description = taxonomy.Description
                };
            }
        );

        properties.InitialTaxonomies = taxonomiesData;

        return Task.FromResult(properties);
    }

    [PageCommand]
    public async Task<ICommandResponse> SaveTaxonomy(TaxonomyCategory data)
    {
        taxonomyProvider.Set(
            new TaxonomyInfo
            {
                DisplayName = data.DisplayName,
                Value = data.Value,
                ParentValue = data.ParentValue,
                ParentID = data.ParentID,
                Description = data.Description
            }
        );

        return Response().AddSuccessMessage("Created Taxonomy");
    }

    [PageCommand]
    public async Task<ICommandResponse<CommandResult>> GetAllTaxonomies()
    {
        IEnumerable<TaxonomyInfo> taxonomyInfo = await taxonomyProvider
            .Get()
            .GetEnumerableTypedResultAsync();

        IEnumerable<TaxonomyCategory> taxonomiesData = taxonomyInfo.Select(
            (taxonomy) =>
            {
                return new TaxonomyCategory
                {
                    ID = taxonomy.TaxonomyID,
                    DisplayName = taxonomy.DisplayName,
                    Value = taxonomy.Value,
                    ParentValue = taxonomy.ParentValue,
                    ParentID = taxonomy.ParentID,
                    Description = taxonomy.Description
                };
            }
        );

        return ResponseFrom(new CommandResult(taxonomiesData));
    }

    [PageCommand]
    public async Task<ICommandResponse> EditTaxonomies(IEnumerable<TaxonomyCategory> data)
    {
        foreach (TaxonomyCategory item in data)
        {
            TaxonomyInfo taxonomy = taxonomyProvider
                .Get()
                .WhereEquals("TaxonomyID", item.ID)
                .FirstOrDefault();
            taxonomy.DisplayName = item.DisplayName;
            taxonomy.ParentID = item.ParentID;
            taxonomy.Value = item.Value;
            taxonomy.ParentValue = item.ParentValue;
            taxonomy.Description = item.Description;

            taxonomyProvider.Set(taxonomy);
        }

        return Response().AddSuccessMessage("Updated Taxonomy");
    }

    [PageCommand]
    public async Task<ICommandResponse> DeleteTaxonomies(IEnumerable<TaxonomyCategory> data)
    {
        foreach (TaxonomyCategory item in data)
        {
            TaxonomyInfo taxonomy = taxonomyProvider
                .Get()
                .WhereEquals("TaxonomyID", item.ID)
                .FirstOrDefault();

            taxonomyProvider.Delete(taxonomy);
        }

        return Response().AddSuccessMessage("Taxonomies Deleted");
    }
}

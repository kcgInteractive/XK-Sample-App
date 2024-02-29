using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
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
    public string GUID { get; set; }

    public string Value { get; set; }
    public string DisplayName { get; set; }
    public string ParentValue { get; set; }
    public string Description { get; set; }
    public string ParentGUID { get; set; }
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
                    GUID = taxonomy.GUID.ToString(),
                    ParentGUID = taxonomy.ParentGUID.ToString(),
                    DisplayName = taxonomy.DisplayName,
                    Value = taxonomy.Value,
                    ParentValue = taxonomy.ParentValue,
                    Description = taxonomy.Description,
                };
            }
        );

        properties.InitialTaxonomies = taxonomiesData;

        return Task.FromResult(properties);
    }

    [PageCommand]
    public ICommandResponse SaveTaxonomy(TaxonomyCategory data)
    {
        try
        {
            taxonomyProvider.Set(
                new TaxonomyInfo
                {
                    DisplayName = data.DisplayName,
                    Value = data.Value,
                    ParentValue = data.ParentValue,
                    ParentGUID = Guid.Parse(data.ParentGUID),
                    Description = data.Description,
                    GUID = Guid.NewGuid()
                }
            );

            return Response().AddSuccessMessage("Created Taxonomy");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
    }

    [PageCommand]
    public ICommandResponse SaveTaxonomies(IEnumerable<TaxonomyCategory> data)
    {
        try
        {
            foreach (TaxonomyCategory item in data)
            {
                taxonomyProvider.Set(
                    new TaxonomyInfo
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        ParentValue = item.ParentValue,
                        ParentGUID = Guid.Parse(item.ParentGUID),
                        Description = item.Description,
                        GUID = Guid.NewGuid()
                    }
                );
            }

            return Response().AddSuccessMessage("Term Created");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
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
                    GUID = taxonomy.GUID.ToString(),
                    DisplayName = taxonomy.DisplayName,
                    Value = taxonomy.Value,
                    ParentValue = taxonomy.ParentValue,
                    ParentGUID = taxonomy.ParentGUID.ToString(),
                    Description = taxonomy.Description
                };
            }
        );

        return ResponseFrom(new CommandResult(taxonomiesData));
    }

    [PageCommand]
    public ICommandResponse EditTaxonomies(IEnumerable<TaxonomyCategory> data)
    {
        try
        {
            foreach (TaxonomyCategory item in data)
            {
                TaxonomyInfo taxonomy = taxonomyProvider
                    .Get()
                    .WhereEquals("GUID", item.GUID)
                    .FirstOrDefault();
                taxonomy.DisplayName = item.DisplayName;
                taxonomy.ParentGUID = Guid.Parse(item.ParentGUID);
                taxonomy.Value = item.Value;
                taxonomy.ParentValue = item.ParentValue;
                taxonomy.Description = item.Description;

                taxonomyProvider.Set(taxonomy);
            }

            return Response().AddSuccessMessage("Term Updated");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
    }

    [PageCommand]
    public ICommandResponse DeleteTaxonomies(IEnumerable<TaxonomyCategory> data)
    {
        try
        {
            foreach (TaxonomyCategory item in data)
            {
                TaxonomyInfo taxonomy = taxonomyProvider
                    .Get()
                    .WhereEquals("GUID", item.GUID)
                    .FirstOrDefault();

                taxonomyProvider.Delete(taxonomy);
            }

            return Response().AddSuccessMessage("Term Deleted");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
    }
}

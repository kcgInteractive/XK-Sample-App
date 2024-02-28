using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.DataEngine;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Site.Web.Admin.UIPages.Taxonomies;
using Taxonomies;

[assembly: RegisterFormComponent(
    "Custom.UIFormComponents.TagSelectorFormComponent",
    typeof(TagSelectorFormComponent),
    "Tag Selector"
)]

public class TagSelectorComponentClientProperties : FormComponentClientProperties<string>
{
    public IEnumerable<TaxonomyCategory> Tags { get; set; }
}

public class TagSelectorComponentAttribute : FormComponentAttribute { }

[ComponentAttribute(typeof(TagSelectorComponentAttribute))]
public class TagSelectorFormComponent : FormComponent<TagSelectorComponentClientProperties, string>
{
    private readonly IInfoProvider<TaxonomyInfo> taxonomyInfo;

    public TagSelectorFormComponent(IInfoProvider<TaxonomyInfo> taxonomyInfo)
    {
        this.taxonomyInfo = taxonomyInfo;
    }

    public override string ClientComponentName => "@sample/web-admin/TagSelector";

    protected override Task ConfigureClientProperties(
        TagSelectorComponentClientProperties clientProperties
    )
    {
        clientProperties.Tags = taxonomyInfo
            .Get()
            .GetEnumerableTypedResult()
            .Select(
                (taxonomyInfo) =>
                {
                    return new TaxonomyCategory
                    {
                        GUID = taxonomyInfo.GUID.ToString(),
                        ParentGUID = taxonomyInfo.ParentGUID.ToString(),
                        DisplayName = taxonomyInfo.DisplayName,
                        Value = taxonomyInfo.Value,
                        ParentValue = taxonomyInfo.ParentValue,
                    };
                }
            );

        return base.ConfigureClientProperties(clientProperties);
    }
}

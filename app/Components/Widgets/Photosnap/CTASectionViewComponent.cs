using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.Core;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Kentico.Xperience.Admin.Base.Forms;
using Microsoft.AspNetCore.Mvc;
using Photosnap;
using Photosnap.Widgets;

[assembly: RegisterWidget(
    identifier: "Photosnap.CTASection",
    viewComponentType: typeof(CTASectionViewComponent),
    propertiesType: typeof(CTASectionProperties),
    name: "CTA Section",
    IconClass = "icon-building-block"
)]

[assembly: Kentico.Forms.Web.Mvc.RegisterFormValidationRule(
    identifier: "Site.Administration.MyValidationCondition",
    validationRuleType: typeof(MyValidationRule),
    name: "Content Value"
)]

namespace Photosnap.Widgets;

public class ValidationOnlyOneContentItemAttribute : ValidationRuleAttribute
{
    public ValidationOnlyOneContentItemAttribute() { }
}

public class MyValidationProperities : ValidationRuleProperties
{
    public override string GetDescriptionText(ILocalizationService localizationService) => "";
}

[ValidationRuleAttribute(typeof(ValidationOnlyOneContentItemAttribute))]
public class MyValidationRule
    : ValidationRule<MyValidationProperities, IEnumerable<ContentItemReference>>
{
    public override Task<ValidationResult> Validate(
        IEnumerable<ContentItemReference> value,
        Kentico.Xperience.Admin.Base.Forms.IFormFieldValueProvider formFieldValueProvider
    )
    {
        // Validation logic

        // 'value' contains the input submitted by the user

        if (value.Count() > 1)
        {
            string errorMessage = "Please select only one content item";
            return ValidationResult.FailResult(errorMessage);
        }

        return ValidationResult.SuccessResult();
    }
}

public class CTASectionProperties : IWidgetProperties
{
    // [ContentItemSelectorComponent(CTAContent.CONTENT_TYPE_NAME, Label = "Choose Data", Order = 1)]
    // public IEnumerable<ContentItemReference> SelectedProperties { get; set; } =
    //     new List<ContentItemReference>();

    [ContentItemSelectorComponent(
        CTAContent.CONTENT_TYPE_NAME,
        Label = "Content",
        ExplanationText = "Please select only one"
    )]
    [ValidationOnlyOneContentItem]
    public IEnumerable<ContentItemReference> Data { get; set; } = new List<ContentItemReference>();
}

public class CTASectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(ComponentViewModel<CTASectionProperties> widgetModel)
    {
        var widgeGUID = widgetModel.Properties.Data.FirstOrDefault().Identifier;
        var builder = new ContentItemQueryBuilder();

        return View("~/Views/Photosnap/_CTASection.cshtml");
    }
}

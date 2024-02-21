using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMS.ContentEngine;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Xperience.Admin.Base.FormAnnotations;
using Microsoft.AspNetCore.Mvc;
using Photosnap.Widgets;

[assembly: RegisterWidget(
    identifier: "Photosnap.CTASection",
    viewComponentType: typeof(CTASectionViewComponent),
    name: "CTA Section",
    IconClass = "icon-building-block"
)]

namespace Photosnap.Widgets;

public class CTASectionProperties : IWidgetProperties
{
    [ContentItemSelectorComponent(CTAContent.CONTENT_TYPE_NAME, Label = "Choose Data", Order = 1)]
    public IEnumerable<ContentItemReference> selectedProperties { get; set; } =
        new List<ContentItemReference>();
}

public class CTASectionViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("~/Views/Photosnap/_CTASection.cshtml");
    }
}

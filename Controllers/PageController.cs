using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine.Internal;
using CMS.MediaLibrary;
using CMS.Websites.Routing;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;
using Shared.Models;
using Site;
using SiteSettingsModule;

[assembly: RegisterWebPageRoute(
    contentTypeName: Shared.Page.CONTENT_TYPE_NAME,
    controllerType: typeof(PageController),
    WebsiteChannelNames = new[] { "Photosnap", "Audiophile" }
)]

namespace Shared.Controllers;

public class PageController : Controller
{
    private readonly IWebsiteChannelContext websiteChannelContext;

    private readonly IMediaFileInfoProvider mediaFileInfoProvider;
    private readonly IMediaFileUrlRetriever mediaFileUrlRetriever;

    public PageController(
        IWebsiteChannelContext websiteChannelContext,
        IMediaFileUrlRetriever mediaFileUrlRetriever,
        IMediaFileInfoProvider mediaFileInfoProvider
    )
    {
        this.websiteChannelContext = websiteChannelContext;
        this.mediaFileUrlRetriever = mediaFileUrlRetriever;
        this.mediaFileInfoProvider = mediaFileInfoProvider;
    }

    public IActionResult Index()
    {
        string ChannelContextName = websiteChannelContext.WebsiteChannelName;

        IEnumerable<SiteSettingsInfo> siteSettings = SiteSettingsInfo.Provider.Get();

        AssetRelatedItem item = JsonDataTypeConverter
            .ConvertToModels<AssetRelatedItem>(siteSettings.FirstOrDefault().mSiteLogo)
            .FirstOrDefault();

        MediaFileInfo mediaInfo = mediaFileInfoProvider
            .Get()
            .WhereEquals("FileGUID", item.Identifier)
            .FirstOrDefault();

        IMediaFileUrl fileUrl = mediaFileUrlRetriever.Retrieve(mediaInfo);

        System.Diagnostics.Debug.WriteLine(fileUrl.RelativePath);

        // System.Diagnostics.Debug.WriteLine(siteSettings?.FirstOrDefault().SiteLogo);

        // foreach (SiteSettings siteSetting in siteSettings)
        // {
        //     AssetRelatedItem item = siteSetting.SiteLogo.FirstOrDefault();
        // }

        // foreach (Page page in pages)
        // {
        //     AssetRelatedItem item = page.Media.FirstOrDefault();
        //     System.Diagnostics.Debug.WriteLine(item?.Name);
        //     Example linkedItem = page?.LinkedItem?.FirstOrDefault();

        //     IEnumerable<MediaFileInfo> mediaFiles = new ObjectQuery<MediaFileInfo>()
        // }

        return View("~/Views/Shared/Page.cshtml", new PageViewModel(ChannelContextName));
    }
}

using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Enable desired Kentico Xperience features
builder.Services.AddKentico(features =>
{
    features.UsePageBuilder(
        new PageBuilderOptions { ContentTypeNames = new[] { Shared.Page.CONTENT_TYPE_NAME } }
    );
    // features.UseActivityTracking();
    features.UseWebPageRouting();
    // features.UseEmailStatisticsLogging();
    // features.UseEmailMarketing();
});

builder.Services.AddRazorPages();

builder.Services.AddAuthentication();

// builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.InitKentico();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseKentico();

// app.UseAuthorization();

app.Kentico().MapRoutes();

// app.MapGet("/", () => "The XK_Boilerplate site has not been configured yet.");

app.Run();

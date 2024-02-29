using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.ContentEngine;
using CMS.DataEngine;
using Kentico.Xperience.Admin.Base;
using Site.Location;
using Site.Web.Admin.UIPages.Locations;
using Site.Web.Admin.UIPages.Taxonomies;
using Taxonomies;

[assembly: UIPage(
    parentType: typeof(LocationsApplication),
    slug: "names",
    uiPageType: typeof(LocationsPage),
    name: "Names",
    templateName: "@sample/web-admin/Locations",
    order: 300
)]

namespace Site.Web.Admin.UIPages.Locations;

public class CommandResult
{
    public IEnumerable<Location> Locations { get; set; }

    public CommandResult(IEnumerable<Location> Locations)
    {
        this.Locations = Locations;
    }
}

public class Location
{
    public string LocationGUID { get; set; }
    public string ChannelGUID { get; set; }
    public string CompanyLocationName { get; set; }

#nullable enable
    public string? CountryCode { get; set; }
    public string? Region { get; set; }

    public string? Country { get; set; }

    public string? StateProvince { get; set; }
    public string? StateProvinceCode { get; set; }

    public string? City { get; set; }
    public string? Street { get; set; }

    public string? Phone { get; set; }
    public string? Tags { get; set; }
#nullable disable
}

public class Channel
{
    public string ChannelGUID { get; set; }
    public string ChannelDisplayName { get; set; }
}

public class PageTemplateClientProperties : TemplateClientProperties
{
    public IEnumerable<Channel> Channels { get; set; }
    public IEnumerable<Location> InitialLocations { get; set; }
    public IEnumerable<TaxonomyCategory> Tags { get; set; }
}

public class LocationsPage : Page<PageTemplateClientProperties>
{
    private readonly IInfoProvider<ChannelInfo> channelProvider;
    private readonly IInfoProvider<LocationsInfo> locationProvider;
    private readonly IInfoProvider<TaxonomyInfo> taxonomyProvider;

    public LocationsPage(
        IInfoProvider<ChannelInfo> channelProvider,
        IInfoProvider<LocationsInfo> locationProvider,
        IInfoProvider<TaxonomyInfo> taxonomyProvider
    )
    {
        this.channelProvider = channelProvider;
        this.locationProvider = locationProvider;
        this.taxonomyProvider = taxonomyProvider;
    }

    public override Task<PageTemplateClientProperties> ConfigureTemplateProperties(
        PageTemplateClientProperties properties
    )
    {
        IEnumerable<ChannelInfo> channelInfo = channelProvider.Get().GetEnumerableTypedResult();

        IEnumerable<Channel> channels = channelInfo.Select(
            (channel) =>
            {
                return new Channel
                {
                    ChannelGUID = channel.ChannelGUID.ToString(),
                    ChannelDisplayName = channel.ChannelDisplayName
                };
            }
        );

        IEnumerable<LocationsInfo> locationInfo = locationProvider.Get().GetEnumerableTypedResult();
        IEnumerable<Location> locations = locationInfo.Select(
            (location) =>
            {
                return new Location
                {
                    ChannelGUID = location.ChannelGUID.ToString(),
                    LocationGUID = location.LocationGUID.ToString(),
                    CompanyLocationName = location.CompanyLocationName,
                    Region = location.Region,
                    CountryCode = location.CountryCode,
                    Country = location.Country,
                    StateProvince = location.State_Province,
                    StateProvinceCode = location.State_ProvinceCode,
                    City = location.City,
                    Street = location.Street,
                    Phone = location.Phone,
                    Tags = location.Tags
                };
            }
        );

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

        properties.Tags = taxonomiesData;
        properties.Channels = channels;
        properties.InitialLocations = locations;

        return Task.FromResult(properties);
    }

    [PageCommand]
    public ICommandResponse SaveLocation(Location data)
    {
        try
        {
            locationProvider.Set(
                new LocationsInfo
                {
                    ChannelGUID = Guid.Parse(data.ChannelGUID),
                    LocationGUID = Guid.NewGuid(),
                    CompanyLocationName = data.CompanyLocationName,
                    Region = data.Region,
                    CountryCode = data.CountryCode,
                    Country = data.Country,
                    State_Province = data.StateProvince,
                    State_ProvinceCode = data.StateProvinceCode,
                    City = data.City,
                    Street = data.Street,
                    Phone = data.Phone,
                    Tags = data.Tags
                }
            );

            return Response().AddSuccessMessage("Location Created");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
        ;
    }

    [PageCommand]
    public async Task<ICommandResponse> GetAllLocations()
    {
        try
        {
            IEnumerable<LocationsInfo> locationInfo = await locationProvider
                .Get()
                .GetEnumerableTypedResultAsync();

            IEnumerable<Location> locationData = locationInfo.Select(
                (data) =>
                {
                    return new Location
                    {
                        ChannelGUID = data.ChannelGUID.ToString(),
                        LocationGUID = data.LocationGUID.ToString(),
                        CompanyLocationName = data.CompanyLocationName,
                        Region = data.Region,
                        CountryCode = data.CountryCode,
                        Country = data.Country,
                        StateProvince = data.State_Province,
                        StateProvinceCode = data.State_ProvinceCode,
                        City = data.City,
                        Street = data.Street,
                        Phone = data.Phone,
                        Tags = data.Tags
                    };
                }
            );
            return ResponseFrom(new CommandResult(locationData));
        }
        catch (Exception e)
        {
            return ResponseFrom(Enumerable.Empty<Location>()).AddErrorMessage(e.Message);
        }
    }

    [PageCommand]
    public ICommandResponse EditLocation(Location data)
    {
        try
        {
            LocationsInfo location = locationProvider
                .Get()
                .WhereEquals("LocationGUID", data.LocationGUID)
                .FirstOrDefault();

            location.CompanyLocationName = data.CompanyLocationName;
            location.Region = data.Region;
            location.CountryCode = data.CountryCode;
            location.Country = data.Country;
            location.State_Province = data.StateProvince;
            location.State_ProvinceCode = data.StateProvinceCode;
            location.City = data.City;
            location.Street = data.Street;
            location.Phone = data.Phone;
            location.Tags = data.Tags;

            locationProvider.Set(location);

            return Response().AddSuccessMessage("Location Updated");
        }
        catch (Exception e)
        {
            return Response().AddSuccessMessage(e.Message);
        }
    }

    [PageCommand]
    public ICommandResponse DeleteLocation(Location data)
    {
        try
        {
            LocationsInfo location = locationProvider
                .Get()
                .WhereEquals("LocationGUID", data.LocationGUID)
                .FirstOrDefault();

            locationProvider.Delete(location);

            return Response().AddSuccessMessage("Location Deleted");
        }
        catch (Exception e)
        {
            return Response().AddErrorMessage(e.Message);
        }
    }
}

namespace AggregatorMicroService.Static;

public static class MicroServiceConfigurationKeys
{
    private const string ApiSectionName = "ApiUrls";

    public const string OfficesUrlKey = $"{ApiSectionName}:OfficesApiUrl";
    public const string PhotosUrlKey = $"{ApiSectionName}:PhotosApiUrl";
    public const string SchedulesUrlKey = $"{ApiSectionName}:SchedulesApiUrl";
    public const string ProfilesUrlKey = $"{ApiSectionName}:ProfilesApiUrl";
    public const string ServicesUrlKey = $"{ApiSectionName}:ServicesApiUrl";
    public const string IdentityUrlKey = $"{ApiSectionName}:IdentityApiUrl";
}
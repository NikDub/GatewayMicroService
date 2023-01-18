namespace AggregatorMicroService.Static;

public class UrlPath
{
    private readonly IConfiguration _configuration;

    public UrlPath(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Offices => _configuration.GetValue<string>(MicroServiceConfigurationKeys.OfficesUrlKey) + "/Offices";
    public string Doctors => _configuration.GetValue<string>(MicroServiceConfigurationKeys.ProfilesUrlKey) + "/Doctors";
    public string Patient => _configuration.GetValue<string>(MicroServiceConfigurationKeys.ProfilesUrlKey) + "/Patients";

    public string Specialization => _configuration.GetValue<string>(MicroServiceConfigurationKeys.ServicesUrlKey) +
                                    "/Specializations";

    public string Documents =>
        _configuration.GetValue<string>(MicroServiceConfigurationKeys.PhotosUrlKey) + "/Documents";

    public string Account => _configuration.GetValue<string>(MicroServiceConfigurationKeys.IdentityUrlKey) + "/Auth";

    public string PatientProfile => Patient + "/profile";
    public string AccountDoctor => Account + "/Doctor";
    public string AccountChangePhoto => Account + "/PhotoChange";
    public string Photo => Documents + "/Photo";
    public string Appointments => _configuration.GetValue<string>(MicroServiceConfigurationKeys.SchedulesUrlKey) + "/Appointments";
}
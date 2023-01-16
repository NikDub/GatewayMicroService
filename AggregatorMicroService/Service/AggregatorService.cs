using AggregatorMicroService.Dto;
using AggregatorMicroService.Dto.Aggregated;
using AggregatorMicroService.Dto.Photos;
using AggregatorMicroService.Dto.Profiles;
using AggregatorMicroService.HttpClient;
using AggregatorMicroService.Service.Abstraction;
using AggregatorMicroService.Static;
using AutoMapper;

namespace AggregatorMicroService.Service;

public class AggregatorService : IAggregatorService
{
    private readonly IMapper _mapper;
    private readonly UrlPath _urlPath;
    private readonly IHttpClient _httpClient;

    public AggregatorService(IMapper mapper, UrlPath urlPath, IHttpClient httpClient)
    {
        _mapper = mapper;
        _urlPath = urlPath;
        _httpClient = httpClient;
    }

    public async Task CreatedPatientProfileWithPhotoByPatient(PatientForCreatedAggregatedDto model,
        string attributeFromHeader)
    {
        var photo = await _httpClient.HttpCreateAsync<PhotoDto, PhotoCreatedDto>(_urlPath.Photo, model.Photo, attributeFromHeader);

        await _httpClient.HttpPutAsync(_urlPath.AccountChangePhoto, model.AccountId, photo.Id, attributeFromHeader);

        var patientMap = _mapper.Map<PatientForCreateDto>(model);
        await _httpClient.HttpCreateAsync<PatientDto, PatientForCreateDto>(_urlPath.PatientProfile, patientMap, attributeFromHeader);
    }

    public async Task CreatedDoctorWithPhotoAndAccountAsync(DoctorForCreatedAggregatedDto model, string attributeFromHeader)
    {
        await _httpClient.HttpGetByIdAsync<OfficeDto>(_urlPath.Offices, model.OfficeId, attributeFromHeader);
        var specialization =
            await _httpClient.HttpGetByIdAsync<SpecializationDto>(_urlPath.Specialization, model.SpecializationId, attributeFromHeader);

        var photo = await _httpClient.HttpCreateAsync<PhotoDto, PhotoCreatedDto>(_urlPath.Photo, model.Photo, attributeFromHeader);

        var accountDto = new DoctorRegistrationDto { Email = model.Email, PhotoId = photo.Id };
        var accountId = await _httpClient.HttpCreateAsync<string, DoctorRegistrationDto>(_urlPath.AccountDoctor, accountDto, attributeFromHeader);

        var doctorForCreateDto = _mapper.Map<DoctorForCreateDto>(model);
        doctorForCreateDto.AccountId = accountId;
        doctorForCreateDto.SpecializationId = specialization.Id;
        doctorForCreateDto.SpecializationName = specialization.Name;
        await _httpClient.HttpCreateAsync<DoctorDto, DoctorForCreateDto>(_urlPath.Doctors, doctorForCreateDto, attributeFromHeader);
    }

    public async Task<List<DoctorWithOfficeDto>> GetDoctorWithOfficeAsync(string attributeFromHeader)
    {
        var offices = await _httpClient.HttpGetAsync<OfficeDto>(_urlPath.Offices, attributeFromHeader);
        var doctors = await _httpClient.HttpGetAsync<DoctorDto>(_urlPath.Doctors, attributeFromHeader);

        if (offices == null || doctors == null)
            return null;

        var doctorWithOfficeList = new List<DoctorWithOfficeDto>();
        foreach (var doctor in doctors)
        {
            var office = offices.FirstOrDefault(r => r.Id == doctor.OfficeId);
            if (office == null)
                continue;
            var doctorWithOffice = _mapper.Map<DoctorWithOfficeDto>(doctor);
            doctorWithOffice.Office = office;
            doctorWithOfficeList.Add(doctorWithOffice);
        }

        return doctorWithOfficeList;
    }

    public async Task<List<AppointmentWithPatientPhoneDto>> GetAppointmentScheduleAsync(string attributeFromHeader)
    {
        var appointments = _httpClient.HttpGetAsync<AppointmentDto>(_urlPath.Appointments, attributeFromHeader);
        var patients = await _httpClient.HttpGetAsync<PatientDto>(_urlPath.Patient, attributeFromHeader);
        if (appointments == null || patients == null)
            return null;

        var appointmentsWithPatientPhone = new List<AppointmentWithPatientPhoneDto>();
        foreach (var appointment in await appointments)
        {
            var patient = patients.FirstOrDefault(r => r.AccountId == appointment.PatientId);
            if (patient == null)
                continue;
            var appointmentsWithPhone = _mapper.Map<AppointmentWithPatientPhoneDto>(appointment);
            appointmentsWithPhone.AccountPhoneNumber = patient.AccountPhoneNumber;
            appointmentsWithPatientPhone.Add(appointmentsWithPhone);
        }

        return appointmentsWithPatientPhone;
    }
}
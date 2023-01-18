using AggregatorMicroService.Dto;
using AggregatorMicroService.Dto.Aggregated;
using AggregatorMicroService.Dto.Photos;
using AggregatorMicroService.Dto.Profiles;
using AggregatorMicroService.Service.Abstraction;
using AggregatorMicroService.Static;
using AutoMapper;

namespace AggregatorMicroService.Service;

public class AggregatorService : IAggregatorService
{
    private readonly IMapper _mapper;
    private readonly UrlPath _urlPath;
    private readonly IHttpService _httpService;

    public AggregatorService(IMapper mapper, UrlPath urlPath, IHttpService httpClient)
    {
        _mapper = mapper;
        _urlPath = urlPath;
        _httpService = httpClient;
    }

    public async Task CreatedPatientProfileWithPhotoByPatient(PatientForCreatedAggregatedDto model,
        string attributeFromHeader)
    {
        var photo = await _httpService.HttpCreateAsync<PhotoDto, PhotoCreatedDto>(_urlPath.Photo, model.Photo, attributeFromHeader);

        await _httpService.HttpPutAsync(_urlPath.AccountChangePhoto, model.AccountId, photo.Id, attributeFromHeader);

        var patientMap = _mapper.Map<PatientForCreateDto>(model);
        await _httpService.HttpCreateAsync<PatientDto, PatientForCreateDto>(_urlPath.PatientProfile, patientMap, attributeFromHeader);
    }

    public async Task CreatedDoctorWithPhotoAndAccountAsync(DoctorForCreatedAggregatedDto model, string attributeFromHeader)
    {
        var office = await _httpService.HttpGetByIdAsync<OfficeDto>(_urlPath.Offices, model.OfficeId, attributeFromHeader);
        var specialization =
            await _httpService.HttpGetByIdAsync<SpecializationDto>(_urlPath.Specialization, model.SpecializationId, attributeFromHeader);

        var photo = await _httpService.HttpCreateAsync<PhotoDto, PhotoCreatedDto>(_urlPath.Photo, model.Photo, attributeFromHeader);

        var accountDto = new DoctorRegistrationDto { Email = model.Email, PhotoId = photo.Id };
        var accountId = await _httpService.HttpCreateAsync<string, DoctorRegistrationDto>(_urlPath.AccountDoctor, accountDto, attributeFromHeader);

        var doctorForCreateDto = _mapper.Map<DoctorForCreateDto>(model);
        doctorForCreateDto.AccountId = accountId;
        doctorForCreateDto.SpecializationId = specialization.Id;
        doctorForCreateDto.SpecializationName = specialization.Name;
        doctorForCreateDto.OfficeId = office.Id;
        await _httpService.HttpCreateAsync<DoctorDto, DoctorForCreateDto>(_urlPath.Doctors, doctorForCreateDto, attributeFromHeader);
    }

    public async Task<IEnumerable<DoctorWithOfficeDto>> GetDoctorWithOfficeAsync(string attributeFromHeader)
    {
        var offices = await _httpService.HttpGetAsync<OfficeDto>(_urlPath.Offices, attributeFromHeader);
        var doctors = await _httpService.HttpGetAsync<DoctorDto>(_urlPath.Doctors, attributeFromHeader);

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

    public async Task<IEnumerable<AppointmentWithPatientPhoneDto>> GetAppointmentScheduleAsync(string attributeFromHeader)
    {
        var appointments = _httpService.HttpGetAsync<AppointmentDto>(_urlPath.Appointments, attributeFromHeader);
        var patients = await _httpService.HttpGetAsync<PatientDto>(_urlPath.Patient, attributeFromHeader);
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
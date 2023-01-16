using AggregatorMicroService.Dto.Aggregated;
using AggregatorMicroService.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace AggregatorMicroService.Controllers;

[ApiController]
[Route("/[controller]")]
public class AggregatorsController : Controller
{
    private readonly IAggregatorService _aggregatorService;
    private const string Authorization = "Authorization";
    public AggregatorsController(IAggregatorService aggregatorService)
    {
        _aggregatorService = aggregatorService;
    }

    [HttpGet("DoctorsWithOffice")]
    public async Task<IActionResult> GetDoctorsWithOffice()
    {
        return Ok(await _aggregatorService.GetDoctorWithOfficeAsync(GetAuthorizationFromHeader()));
    }

    [HttpPost("Doctor")] // TODO redirect
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorForCreatedAggregatedDto model)
    {
        await _aggregatorService.CreatedDoctorWithPhotoAndAccountAsync(model, GetAuthorizationFromHeader());
        return Created("", null);
    }

    [HttpPost("PatientProfile")] // TODO redirect
    public async Task<IActionResult> CreatePatientProfile([FromBody] PatientForCreatedAggregatedDto model)
    {
        await _aggregatorService.CreatedPatientProfileWithPhotoByPatient(model, GetAuthorizationFromHeader());
        return Created("", null);
    }

    [HttpGet("Appointments/Schedule")]
    public async Task<IActionResult> GetAppointmentSchedule()
    {
        return Ok(await _aggregatorService.GetAppointmentScheduleAsync(GetAuthorizationFromHeader()));
    }
    private string GetAuthorizationFromHeader()
    {
        if (Request.Headers.TryGetValue(Authorization, out var attributeFromHeader))
        {
            return attributeFromHeader;
        }

        throw new UnauthorizedAccessException("");
    }
}
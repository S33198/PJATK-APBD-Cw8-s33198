using Hospital2.DTO;
using Hospital2.Models;
using Hospital2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hospital2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientServices)
    {
        _patientService = patientServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients([FromQuery]string? search)
    {
        var patients = await _patientService.GetPatientsAsync(search);
        return Ok(patients);
    }

    [HttpPost("{id}/bedassignments")]
    public async Task<IActionResult> AssignBed([FromRoute]string id, CreateBedAssigmentDTO DTO)
    {
        try
        {
            await _patientService.AssignBedAsync(id, DTO);
            return Ok();
        }
        catch (Exception e)
        {
            if(e.Message == "No available beds")
                return NotFound("No available beds");
            if(e.Message == "Patient not found")
                return NotFound("Patient not found");
            if(e.Message == "Start date must be before end date")
                return BadRequest("Start date must be before end date");
            Console.WriteLine(e);
            return StatusCode(500, "An unexpected error occured");
        }
    }
        
}
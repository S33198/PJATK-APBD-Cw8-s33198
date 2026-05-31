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
    public async Task<IActionResult> GetAllPatients(string? search)
    {
        var patients = await _patientService.GetPatientsAsync(search);
        return Ok(patients);
    }

    [HttpPost("{id}/bedassignments")]
    public async Task<IActionResult> AssignBed(string id, CreateBedAssigmentDTO DTO)
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
            Console.WriteLine(e);
            throw;
        }
    }
        
}
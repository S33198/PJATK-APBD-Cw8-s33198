using Hospital2.DTO;
using Hospital2.Models;

namespace Hospital2.Services;

public interface IPatientService
{
    Task<IEnumerable<AllPatientsDTO>> GetPatientsAsync(string? name);
    Task AssignBedAsync(string pesel, CreateBedAssigmentDTO DTO);
}
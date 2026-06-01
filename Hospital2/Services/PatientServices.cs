using Hospital2.DTO;
using Hospital2.Models;
using Microsoft.EntityFrameworkCore;

namespace Hospital2.Services;

public class PatientServices : IPatientService
{
    private readonly DatabaseContext _context;

    public PatientServices(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AllPatientsDTO>> GetPatientsAsync(string? name)
    {
        var query = _context.Patients.AsQueryable();
        if (!string.IsNullOrEmpty(name))
            query = query.Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name));
        var patients = await query.Select(p => new AllPatientsDTO
        {
            Age = p.Age,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Pesel = p.Pesel,
            Sex = p.Sex
        }).ToListAsync();
        if(patients.Count == 0)
            throw new Exception("Not found");
        return patients;
    }
    

    public async Task AssignBedAsync(string pesel, CreateBedAssigmentDTO DTO)
    {
        var patient = await _context.Patients.FindAsync(pesel);
        if(patient == null)
            throw new Exception("Patient not found");
        if(DTO.to !=null && DTO.from > DTO.to)
            throw new Exception("Start date must be before end date");
        var availablebeds = await _context.Beds.
            Include(b=> b.Room).
            Include(b=> b.Room.Ward).
            Include(b=> b.BedType).
            Where(b=> b.BedType.Name == DTO.bedType && b.Room.Ward.Name== DTO.ward).
            FirstOrDefaultAsync(b=>!b.BedAssignments.Any(ba=>ba.From < DTO.to && ba.To > DTO.from));
        if(availablebeds == null)
            throw new Exception("No available beds");
        if(DTO.from < new DateTime(2000, 1, 1) )
            throw new Exception("Invalid date");
        if(DTO.to != null)
            if(DTO.to < new DateTime(2000, 1, 1))
                throw new Exception("Invalid date");
        var assigment = new BedAssignment
        {
            PatientPesel = patient.Pesel,
            BedId = availablebeds.Id,
            From = DTO.from,
            To = DTO.to
        };
        _context.BedAssignments.Add(assigment);
        await _context.SaveChangesAsync();
    }
}
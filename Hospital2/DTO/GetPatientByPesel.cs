namespace Hospital2.DTO;

public class GetPatientByPesel
{
    public string Pesel { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public bool Sex { get; set; }
}
namespace Hospital2.DTO;

public class BedAssigmenDTO
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public BedDTO bed { get; set; }
}
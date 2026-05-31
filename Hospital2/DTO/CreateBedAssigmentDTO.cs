using System.ComponentModel.DataAnnotations;

namespace Hospital2.DTO;

public class CreateBedAssigmentDTO
{
    [Required]
    public DateTime from { get; set; }
    public DateTime? to { get; set; }
    public string bedType { get; set; }
    public string ward { get; set; }
    
}
namespace Hospital2.DTO;

public class BedTypeDTO
{
    public int Id { get; set; }
    public BedTypeDTO BedType { get; set; }
    public RoomDTO Room { get; set; }
}
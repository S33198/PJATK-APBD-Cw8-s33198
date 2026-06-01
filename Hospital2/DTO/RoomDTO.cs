namespace Hospital2.DTO;

public class RoomDTO
{
    public string Id { get; set; }
    public bool HasTV { get; set; }
    public WardDTO ward { get; set; }
}
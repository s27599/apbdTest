namespace WebApplication1.Models;

public class MusicianDTO
{
    public int MusicianId { get; set; }
    public String name { get; set; }
    public String surname { get; set; }
    public String? nickname { get; set; }
    public List<Song> songs { get; set; }
}
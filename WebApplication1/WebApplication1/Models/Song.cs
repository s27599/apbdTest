namespace WebApplication1.Models;

public class Song
{
    public Song()
    {
    }

    public Song(int songId, string songName, float duration, int albumId)
    {
        SongId = songId;
        SongName = songName;
        Duration = duration;
        AlbumId = albumId;
    }

    public int SongId { get; set; }
    public String SongName { get; set; }
    public float Duration { get; set; }
    public int? AlbumId { get; set; }
}
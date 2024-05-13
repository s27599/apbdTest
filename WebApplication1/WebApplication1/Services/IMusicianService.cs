using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IMusicianService
{
    public Task<MusicianDTO> GetMusician(int id);
    public Task<bool> MusicanExists(int id);
}
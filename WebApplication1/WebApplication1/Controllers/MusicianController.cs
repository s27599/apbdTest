using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api")]
public class MusicianController : ControllerBase
{
    private readonly IMusicianService _musicianService;

    public MusicianController(IMusicianService musicianService)
    {
        _musicianService = musicianService;
    }

    [HttpGet]
    public async Task<IActionResult> getMusician(int id)
    {
        if (!await _musicianService.MusicanExists(id))
        {
            return NotFound("Musician doesn't exist.");
        }
        

        return Ok(_musicianService.GetMusician(id));

    }
}
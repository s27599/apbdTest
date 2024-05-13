using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class MusicianService : IMusicianService
{
    private readonly IConfiguration _configuration;

    public MusicianService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<MusicianDTO> GetMusician(int id)
    {
        await using SqlConnection connection =
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand();
        await using SqlCommand command2 = new SqlCommand();
        await connection.OpenAsync();
        command2.Connection = connection;
        command2.CommandText = "select Utwor.idutwor,Utwor.nazwautworu,Utwor.czastrwania,Utwor.idalbum from Utwor " +
                               "INNER JOIN WykonawcaUtworu  on Utwor.IdUtwor = WykonawcaUtworu.IdUtwor WHERE WykonawcaUtworu.IdMuzyk = @MusicianId2";
        command2.Parameters.AddWithValue("@MusicianId2", id);
        var reader2 = await command2.ExecuteReaderAsync();
        List<Song> songs = new List<Song>();
        while (await reader2.ReadAsync())
        {
            songs.Add(new Song
            {
                SongId = reader2.GetInt32(reader2.GetOrdinal("IdUtwor")),
                SongName = reader2.GetString(reader2.GetOrdinal("NazwaUtworu")),
                Duration = reader2.GetFloat(reader2.GetOrdinal("CzasTrwania")),
                AlbumId = reader2.GetInt32(reader2.GetOrdinal("IdAlbum")),
            });
        }


        command.Connection = connection;
        command.CommandText = "SELECT * FROM Muzyk WHERE idMuzyk = @MusicianId";
        command.Parameters.AddWithValue("@MusicianId", id);


        var reader = await command.ExecuteReaderAsync();

        return new MusicianDTO
        {
            MusicianId = reader.GetInt32(reader.GetOrdinal("IdMuzyk")),
            name = reader.GetString(reader.GetOrdinal("Imie")),
            surname = reader.GetString(reader.GetOrdinal("Nazwisko")),
            nickname = reader.GetString(reader.GetOrdinal("Pseudonim")),
            songs = songs
        };
    }

    public async Task<bool> MusicanExists(int id)
    {
        await using SqlConnection connection =
            new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "SELECT 1 FROM Muzyk WHERE idMuzyk = @MusicianId";
        command.Parameters.AddWithValue("@MusicianId", id);
        await connection.OpenAsync();
        var reader = await command.ExecuteReaderAsync();
        return reader is not null;
    }
}
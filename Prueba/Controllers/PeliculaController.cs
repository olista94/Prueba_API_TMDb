using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Prueba.Models;
using System.Configuration;

namespace Prueba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeliculaController : ControllerBase
    {
        // ApiKey
        private const string TMDbApiKey = "2485efbdb1367ebd2417ba38d297df22";

        // Console.WriteLine("-- BUSQUEDA DE PELICULAS --");
        // Console.WriteLine("Escribe la pelicula que deseas buscar: ");
        // String texto;
        // texto = Console.ReadLine();

        // [HttpGet("{titulo}")]
        [HttpGet]
        //public async Task<IActionResult> ObtenerInformacionPelicula(string titulo)
        public IActionResult ObtenerInformacionPelicula([FromQuery] string titulo)
        {
            if (string.IsNullOrEmpty(titulo))
            {
                return BadRequest(new { error = "Por favor, proporciona el título de la película." });
            }

            // var detallesPelicula = await ObtenerDetallesPelicula(titulo);
            var detallesPelicula = ObtenerDetallesPelicula(titulo).Result;

            if (detallesPelicula == null)
            {
                return NotFound(new { error = "Película no encontrada" });
            }

            // var peliculasSimilares = await ObtenerPeliculasSimilares(detallesPelicula.Id);
            var peliculasSimilares = ObtenerPeliculasSimilares(detallesPelicula.Id).Result;


            var respuesta = new
            {
                Título = detallesPelicula.title,
                TítuloOriginal = detallesPelicula.original_title,
                PuntuaciónMedia = detallesPelicula.vote_average,
                FechaEstreno = detallesPelicula.release_date,
                Sinopsis = detallesPelicula.overview,
                PelículasSimilares = ObtenerListaPeliculasSimilares(peliculasSimilares)
            };

            return Ok(respuesta);
        }

        private async Task<Pelicula> ObtenerDetallesPelicula(string titulo)
        {
            using var httpClient = new HttpClient();
            var url = $"https://api.themoviedb.org/3/search/movie?api_key={TMDbApiKey}&query={titulo}";
            var respuesta = await httpClient.GetStringAsync(url);

            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<TMDbSearchResult<Pelicula>>(respuesta);
            return resultado.Results.FirstOrDefault();
        }

        private async Task<List<Pelicula>> ObtenerPeliculasSimilares(int peliculaId)
        {
            using var httpClient = new HttpClient();
            var url = $"https://api.themoviedb.org/3/movie/{peliculaId}/similar?api_key={TMDbApiKey}";
            var respuesta = await httpClient.GetStringAsync(url);

            var resultado = Newtonsoft.Json.JsonConvert.DeserializeObject<TMDbSearchResult<Pelicula>>(respuesta);
            return resultado.Results;
        }

        private string ObtenerListaPeliculasSimilares(List<Pelicula> peliculasSimilares)
        {
            if (peliculasSimilares == null || !peliculasSimilares.Any())
            {
                return null;
            }

            return string.Join(", ", peliculasSimilares.Take(5).Select(p => $"{p.title} ({ObtenerAñoEstreno(p.release_date)})"));
        }

        private int ObtenerAñoEstreno(string fechaEstreno)
        {
            if (DateTime.TryParse(fechaEstreno, out var fecha))
            {
                return fecha.Year;
            }
            return 0;
        }
    }
}

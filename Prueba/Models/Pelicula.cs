namespace Prueba.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string title { get; set; }

        public string original_title { get; set; }

        public double vote_average { get; set; }

        public string release_date { get; set; }

        public string overview { get; set; }

    }

    public class TMDbSearchResult<T>
    {
        public List<T> Results { get; set; }
    }
}

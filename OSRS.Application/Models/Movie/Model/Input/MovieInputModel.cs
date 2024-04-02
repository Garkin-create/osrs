using System.Collections.Generic;

namespace OSRS.Application.Models.Movie.Model.Input
{
    public class MovieInputModel
    {
        public string Title { get; set; }
        public string SpanishTitle { get; set; }
        public int Genre { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public string Rating { get; set; }
        public string Synopsis { get; set; }
        // public List<string> MainActors { get; set; }
        public string ProductionStudio { get; set; }
        public string ProductionCountry { get; set; }
        public string PosterUrl { get; set; }
        // public List<string> Observations { get; set; }
    }
    
    
}
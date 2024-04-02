using System;
using System.Collections.Generic;
using System.Globalization;
using OSRS.Core.Models.Enums;

namespace OSRS.Domain.Entities.Movie
{
    public class MovieObject: Entity <long>
    {
        public string Title { get; set; }
        public string SpanishTitle { get; set; }
        public int Genre { get; set; }
        public string Director { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public string Rating { get; set; }
        public string Synopsis { get; set; }
        public string MainActors { get; set; }
        public string ProductionStudio { get; set; }
        public string ProductionCountry { get; set; }
        public string PosterUrl { get; set; }
        public string Observations { get; set; }
    }
}
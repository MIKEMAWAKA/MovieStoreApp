using System;
using System.ComponentModel.DataAnnotations;

namespace MovieStoreApp.Models.Domain
{
	public class Movie
	{
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? ReleaseYear { get; set; }


        [Required]
        public string MovieImage { get; set; } //movie image with extension

        [Required]
        public string? Cast { get; set; }

        [Required]
        public string? Director { get; set; }
    }
}


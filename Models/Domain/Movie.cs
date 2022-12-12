using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieStoreApp.Models.Domain
{
	public class Movie
	{
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? ReleaseYear { get; set; }


      
        public string? MovieImage { get; set; } //movie image with extension

        [Required]
        public string? Cast { get; set; }

        [Required]
        public string? Director { get; set; }

        //[Required]
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        [NotMapped]
        public List<int>? Genres { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? GenreList { get; set; }



        [NotMapped]
        public string? GenreNames { get; set; }

        [NotMapped]
        public MultiSelectList? MultiGenreList { get; set; }



    }
}


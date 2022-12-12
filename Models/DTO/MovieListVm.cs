using System;
using MovieStoreApp.Models.Domain;

namespace MovieStoreApp.Models.DTO
{
	public class MovieListVm
	{

		public IQueryable<Movie> MovieList { get; set; }

	}
}


using System;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Models.DTO;

namespace MovieStoreApp.Repository.Abstract
{
	public interface IMovieService
	{

        bool Add(Movie movie);

        bool Update(Movie movie);

        Movie GetById(int id);

        bool Delete(int id);

        MovieListVm List(string term = "", bool paging = false, int currentPage = 0);

        List<int> GetGenreByMovieId(int movieId);
    }
}


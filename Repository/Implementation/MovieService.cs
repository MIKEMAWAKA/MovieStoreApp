using System;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Models.DTO;
using MovieStoreApp.Repository.Abstract;

namespace MovieStoreApp.Repository.Implementation
{
	public class MovieService : IMovieService
    {
        private readonly MovieDatabaseContext ctx;

        public MovieService(MovieDatabaseContext ctx)
		{
            this.ctx = ctx;
        }

        public bool Add(Movie movie)
        {
            try
            {

                ctx.Movies.Add(movie);
                ctx.SaveChanges();
                foreach (int genreId in movie.Genres)
                {
                    var movieGenre = new MovieGenre
                    {
                        MovieId = movie.Id,
                        GenreId = genreId
                    };
                    ctx.MovieGenres.Add(movieGenre);
                }
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;

                var moviegenres = ctx.MovieGenres.Where(a => a.MovieId == data.Id);

                foreach (var item in moviegenres)
                {
                    ctx.MovieGenres.Remove(item);

                }

                ctx.Movies.Remove(data);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Movie GetById(int id)
        {
            var data = ctx.Movies.Find(id);

            return data;
        }

        public MovieListVm List()
        {
            var list  = ctx.Movies.ToList();

            foreach (var item in  list)
            {
                var genres = (from genre in ctx.Genres
                              join
                              mg in ctx.MovieGenres
                              on genre.Id equals mg.GenreId
                              where mg.MovieId == item.Id
                              select genre.GenreName
                              ).ToList();

                var genreNames = string.Join(',', genres);

                item.GenreNames = genreNames;

            }


            var data = new MovieListVm
            {
                MovieList = list.AsQueryable()

            };

            return data;
        }

        public bool Update(Movie movie)
        {
            try
            {
                var genresToDeleted = ctx.MovieGenres.Where(a => a.MovieId == movie.Id && !movie.Genres.Contains(a.GenreId)).ToList();
                foreach (var mGenre in genresToDeleted)
                {
                    ctx.MovieGenres.Remove(mGenre);
                }
               


                foreach (int item in movie.Genres)
                {
                    var movieGenre = ctx.MovieGenres.FirstOrDefault(a => a.MovieId == movie.Id && a.GenreId == item);

                    if(movieGenre == null)
                    {
                        movieGenre = new MovieGenre
                        {
                            GenreId = item,
                            MovieId = movie.Id
                        };
                        ctx.MovieGenres.Add(movieGenre);
                    }

                }
                ctx.Movies.Update(movie);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = ctx.MovieGenres.Where(a => a.MovieId == movieId)
                .Select(a => a.GenreId).ToList();

            return genreIds;

        }
    }
}


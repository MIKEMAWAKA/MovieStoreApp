using System;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Repository.Abstract;

namespace MovieStoreApp.Repository.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly MovieDatabaseContext ctx;

        public GenreService(MovieDatabaseContext ctx)
        {
            this.ctx = ctx;
        }
    

        public bool Add(Genre genre)
        {
            try
            {
                ctx.Genres.Add(genre);
                ctx.SaveChanges();
               return true;

            }
            catch(Exception ex)
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
                ctx.Genres.Remove(data);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Genre GetById(int id)
        {
            var data = ctx.Genres.Find(id);

            return data;
        }

        public IQueryable<Genre> List()
        {
            var data = ctx.Genres.AsQueryable();

            return data;
        }

        public bool Update(Genre genre)
        {
            try
            {
                ctx.Genres.Update(genre);
                ctx.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}


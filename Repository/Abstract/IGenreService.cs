using System;
using MovieStoreApp.Models.Domain;

namespace MovieStoreApp.Repository.Abstract
{
	public interface IGenreService
	{

		bool Add(Genre genre);

		bool Update(Genre genre);

		Genre GetById(int id);

        bool Delete(int id);

		IQueryable<Genre> List();


    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Repository.Abstract;
using MovieStoreApp.Repository.Implementation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStoreApp.Controllers
{

    [Authorize]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;
        private readonly IFileService fileService;

        public MovieController(IMovieService movieService, IGenreService genreService,IFileService fileService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.fileService = fileService;
        }
        // GET: /<controller>/
        public IActionResult Add()
        {

            var model = new Movie();
            model.GenreList = genreService.List().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text=a.GenreName,Value=a.Id.ToString()

            });

            

            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Movie model)
        {
            model.GenreList = genreService.List().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = a.GenreName,
                Value = a.Id.ToString()

            });
            if (!ModelState.IsValid)
            {
                TempData["msg"] = "No all data added to model";

                return View(model);

            }

            if (model.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.MovieImage = imageName;
            }
            var result = movieService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var model = movieService.GetById(id);
            //model.GenreList = genreService.List().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //{
            //    Text = a.GenreName,
            //    Value = a.Id.ToString()

            //});

            var selectedGenres = movieService.GetGenreByMovieId(model.Id);

            MultiSelectList multiSelectList = new MultiSelectList(genreService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiSelectList;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Movie model)
        {


            var selectedGenres = movieService.GetGenreByMovieId(model.Id);

            MultiSelectList multiSelectList = new MultiSelectList(genreService.List(), "Id", "GenreName", selectedGenres);
            model.MultiGenreList = multiSelectList;


            if (!ModelState.IsValid)
                return View(model);
            model.GenreList = genreService.List().Select(a => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = a.GenreName,
                Value = a.Id.ToString()

            });

            if (model.ImageFile != null)
            {
                var fileReult = this.fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.MovieImage = imageName;
            }
            var result = movieService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(MovieList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult MovieList()
        {
            var data = this.movieService.List();

            //return Ok(data);

            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = movieService.Delete(id);
            return RedirectToAction(nameof(MovieList));
        }
    }
}

   



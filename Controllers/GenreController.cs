using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApp.Models.Domain;
using MovieStoreApp.Repository.Abstract;
using MovieStoreApp.Repository.Implementation;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStoreApp.Controllers
{

    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }
        // GET: /<controller>/
        public IActionResult  Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = genreService.Add(model);

            if (result)
            {
                TempData["msg"] = "Success Genre Added" ;


                return RedirectToAction(nameof(Add));

            }
            else
            {
                TempData["msg"] = "Error on server side..";


                return View();

            }
          
        
        }

        public IActionResult Edit(int id)
        {
            var data = genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult GenreList()
        {
            var data = this.genreService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }
    }
}


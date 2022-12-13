using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApp.Repository.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService movieService;

        public HomeController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        // GET: /<controller>/
        public IActionResult Index(string term ="",int currentPage = 1)
        {
            var data = this.movieService.List(term,true,currentPage);

            //return Ok(data);

            return View(data);
            //return View();
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult MovieDetail(int MovieId)
        {
            var movie = movieService.GetById(MovieId);
            return View(movie);
        }
    }
}


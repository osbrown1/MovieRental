using Microsoft.AspNetCore.Mvc;
using MovieRental.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieRental.Controllers
{
  public class MoviesController : Controller
  {
    private readonly MovieRentalContext _db;

    public MoviesController(MovieRentalContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Movies.ToList());
    }

    public ActionResult Details(int id)
    {
      Movie thisMovie = _db.Movies
          .Include(movie => movie.JoinEntities)
          .ThenInclude(join => join.Customer)
          .FirstOrDefault(movie => movie.MovieId == id);
      return View(thisMovie);
    }

    public ActionResult Create()
    {
      ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Movie movie, int customerId)
    {
      _db.Movies.Add(movie);
      _db.SaveChanges();
      #nullable enable
      CustomerMovie? joinEntity = _db.CustomerMovies.FirstOrDefault(join => (join.CustomerId == customerId && join.MovieId == movie.MovieId));
      #nullable disable
      if (joinEntity == null && customerId != 0)
      {
        _db.CustomerMovies.Add(new CustomerMovie() { CustomerId = customerId, MovieId = movie.MovieId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddCustomer(int id)
    {
      Movie thisMovie = _db.Movies.FirstOrDefault(movies => movies.MovieId == id);
      ViewBag.CustomerId = new SelectList(_db.Customers, "CustomerId", "Name");
      return View(thisMovie);
    }

    [HttpPost]
    public ActionResult AddCustomer(Movie movie, int customerId)
    {
#nullable enable
      CustomerMovie? joinEntity = _db.CustomerMovies.FirstOrDefault(join => (join.CustomerId == customerId && join.MovieId == movie.MovieId));
#nullable disable
      if (joinEntity == null && customerId != 0)
      {
        _db.CustomerMovies.Add(new CustomerMovie() { CustomerId = customerId, MovieId = movie.MovieId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = movie.MovieId });
    }

    public ActionResult Edit(int id)
    {
      Movie thisMovie = _db.Movies.FirstOrDefault(movies => movies.MovieId == id);
      return View(thisMovie);
    }

    [HttpPost]
    public ActionResult Edit(Movie movie)
    {
      _db.Movies.Update(movie);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Movie thisMovie = _db.Movies.FirstOrDefault(movies => movies.MovieId == id);
      return View(thisMovie);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Movie thisMovie = _db.Movies.FirstOrDefault(movies => movies.MovieId == id);
      _db.Movies.Remove(thisMovie);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      CustomerMovie joinEntry = _db.CustomerMovies.FirstOrDefault(entry => entry.CustomerMovieId == joinId);
      _db.CustomerMovies.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
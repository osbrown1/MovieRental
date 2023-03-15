using Microsoft.AspNetCore.Mvc;
using MovieRental.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.Controllers
{
  public class HomeController : Controller
  {
    private readonly MovieRentalContext _db;
    public HomeController(MovieRentalContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      Movie[] movies = _db.Movies.ToArray(); // if this doesn't work, rename movies
      Customer[] customers = _db.Customers.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("movies", movies);
      model.Add("customers", customers);
      return View(model);
    }
  }
}
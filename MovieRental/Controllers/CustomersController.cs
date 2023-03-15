using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MovieRental.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieRental.Controllers
{
  public class CustomersController : Controller
  {
    private readonly MovieRentalContext _db;

    public CustomersController(MovieRentalContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Customer> model = _db.Customers.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Customer customer)
    {
      _db.Customers.Add(customer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    [HttpPost]
    public ActionResult Edit(Customer customer)
    {
      _db.Customers.Update(customer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Customer thisCustomer = _db.Customers
                                .Include(customer => customer.JoinEntities)
                                .ThenInclude(join => join.Movie)
                                .FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    public ActionResult AddMovie(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(Customers => Customers.CustomerId == id);
      ViewBag.MovieId = new SelectList(_db.Movies, "MovieId", "Name");
      return View(thisCustomer);
    }

    [HttpPost]
    public ActionResult AddMovie(Customer customer, int movieId)
    {
  #nullable enable
      CustomerMovie? joinEntity = _db.CustomerMovies.FirstOrDefault(join => (join.MovieId == movieId && join.CustomerId == customer.CustomerId));
  #nullable disable
      if (joinEntity == null && movieId != 0)
      {
        _db.CustomerMovies.Add(new CustomerMovie() { MovieId = movieId, CustomerId = customer.CustomerId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = customer.CustomerId });
    }

    public ActionResult Delete(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      return View(thisCustomer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Customer thisCustomer = _db.Customers.FirstOrDefault(customer => customer.CustomerId == id);
      _db.Customers.Remove(thisCustomer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
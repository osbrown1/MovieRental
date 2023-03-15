using Microsoft.EntityFrameworkCore;

namespace MovieRental.Models
{
    public class MovieRentalContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<CustomerMovie> CustomerMovies { get; set; }
        public MovieRentalContext(DbContextOptions options) : base(options) { }
    }
}
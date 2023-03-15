using System.Collections.Generic;

namespace MovieRental.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int NumberInStock { get; set; }
        public List<CustomerMovie> JoinEntities { get; set; }
    }
}
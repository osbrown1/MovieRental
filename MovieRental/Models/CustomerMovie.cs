namespace MovieRental.Models
{
    public class CustomerMovie
    {
      public int CustomerMovieId { get; set; }
        public int CustomerId { get; set; }
        public int MovieId { get; set; }
        public Customer Customer { get; set; }
        public Movie Movie { get; set; }
    }
}
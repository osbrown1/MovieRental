using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string MembershipType { get; set; }
        public List<CustomerMovie> JoinEntities { get; set; }
    }
}
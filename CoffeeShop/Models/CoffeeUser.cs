using System;
using System.Collections.Generic;

namespace CoffeeShop.Models
{
    public partial class CoffeeUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string UserPassword { get; set; }
        public int UserId { get; set; }
        public decimal Funds { get; set; }
    }
}

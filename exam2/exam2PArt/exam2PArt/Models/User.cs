using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exam2PArt.Models
{
    public class User : IdentityUser
    {
        public List<Order> Orders { get; set; }
        public Cart Cart { get; set; }
    }
}

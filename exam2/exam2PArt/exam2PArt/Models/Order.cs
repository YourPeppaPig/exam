using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exam2PArt.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public List<Dishes> Dishes { get; set; }
        public int FinalPrice { get; set; }
        public Restaurants Restaurant { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

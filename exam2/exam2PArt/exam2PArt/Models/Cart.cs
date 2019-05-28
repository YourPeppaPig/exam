using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exam2PArt.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public List<Dishes> Dishes { get; set; }
        public Restaurants Restaurant { get; set; }
    }
}

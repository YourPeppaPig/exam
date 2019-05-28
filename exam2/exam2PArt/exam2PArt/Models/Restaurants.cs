using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace exam2PArt.Models
{
    public class Restaurants
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Dishes> Menu { get; set; }
    }
}

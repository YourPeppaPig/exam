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
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<string> DishesId { get; set; }
        public int FinalPrice { get; set; }
    }
}

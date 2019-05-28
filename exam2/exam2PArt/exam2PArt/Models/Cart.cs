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
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<int> DishesId { get; set; }
        public int Cost { get; set; }
        public void AddDish(Dishes dish)
        {
            DishesId.Add(dish.Id);
            Cost += dish.CostOfMeal;
        }
    }
}

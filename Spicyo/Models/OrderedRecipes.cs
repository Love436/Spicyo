using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Spicyo.Models
{
    public class OrderedRecipes
    {
        public int Id { get; set; }

        [Display(Name = "Recipe")]
        public string Recipe { get; set; }

        [Display(Name = "RecipeType")]
        public string RecipeType { get; set; }

        [Display(Name = "Price")]
        public string Price { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }
    }
}

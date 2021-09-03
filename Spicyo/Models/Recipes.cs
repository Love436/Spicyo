using System.ComponentModel.DataAnnotations;

namespace Spicyo.Models
{
    public class Recipes
    {
        public int Id { get; set; }

        [Display(Name = "Recipe")]
        public string Recipe { get; set; }

        [Display(Name = "RecipeType")]
        public string RecipeType { get; set; }
        
        [Display(Name = "Price")]
        public string Price { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CookRE.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Overview { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public string Steps { get; set; }
        public string? ImageUrl { get; set; }
        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }
    }
}

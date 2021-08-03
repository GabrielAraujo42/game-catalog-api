using System;
using System.ComponentModel.DataAnnotations;

namespace GameCatalogAPI.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game's name must have between 3 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The developer's name must have between 3 and 100 characters.")]
        public string Developer { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The game's price must be between 1 and 1000 reais.")]
        public double Price { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BeerExampleApi.Models
{
    public class Beer
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Range(0, 100)]
        public float Percentage { get; set; }
    }
}
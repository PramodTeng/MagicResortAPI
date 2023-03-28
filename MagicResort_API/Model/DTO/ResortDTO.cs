using System.ComponentModel.DataAnnotations;

namespace MagicResort_API.Model.DTO
{
    public class ResortDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }

        public int Sqft { get; set; }
    }
}


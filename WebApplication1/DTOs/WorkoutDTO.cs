using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int BurnedCcal { get; set; }
    }
}

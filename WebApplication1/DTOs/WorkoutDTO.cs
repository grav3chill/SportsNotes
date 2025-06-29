using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Тип тренировки не должен превышать 100 символов")]
        public string? Type { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 2000, ErrorMessage = "не обманывай :)")]
        public int BurnedCcal { get; set; }
    }
}

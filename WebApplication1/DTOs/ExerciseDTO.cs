using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string? MuscleGroup { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string? Name { get; set; }
        
        [Required]
        public int Sets { get; set; }

        [Required]
        public int Reps { get; set; }

        [Required]
        public float Weight { get; set; }
    }
}

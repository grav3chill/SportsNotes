using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        
        [Required]
        public string? MuscleGroup { get; set; }

        [Required]
        public string? Name { get; set; }
        
        [Required]
        public int Sets { get; set; }

        [Required]
        public int Reps { get; set; }

        [Required]
        public float Weight { get; set; }
    }
}

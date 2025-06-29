namespace SportsNotes.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }
        public string? MuscleGroup { get; set; }
        public string? Name { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float Weight { get; set; }
    }
}

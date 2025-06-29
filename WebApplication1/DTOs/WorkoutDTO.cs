namespace SportsNotes.DTOs
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public DateTime Date { get; set; }
        public int BurnedCcal { get; set; }
    }
}

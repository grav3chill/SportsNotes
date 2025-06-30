using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class ProgressRecordDTO
    {
        public int Id { get; set; }

        [Required]
        public int WeightProgress { get; set; }

        [Required]
        public string? Overall { get; set; }
    }
}

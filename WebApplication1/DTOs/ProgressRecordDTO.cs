using System.ComponentModel.DataAnnotations;

namespace SportsNotes.DTOs
{
    public class ProgressRecordDTO
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 300, ErrorMessage = "не обманывай :)")]
        public int WeightProgress { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Слишком много символов")]
        public string? Overall { get; set; }
    }
}

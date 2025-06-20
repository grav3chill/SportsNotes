using SportsNotes.DTOs;

namespace SportsNotes.Interfaces
{
    public interface IProgressRecordService
    {
        IEnumerable<ProgressRecordDTO> GetAllProgressRecords();
        ExerciseDTO GetProgressRecordById(int id);
        ProgressRecordDTO AddProgressRecord(ProgressRecordDTO progressRecordDTO);
        ProgressRecordDTO EditProgressRecord(int id, ProgressRecordDTO progressRecordDTO);
        void DeleteProgressRecord(int id);
    }
}

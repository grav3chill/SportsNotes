using SportsNotes.DTOs;

namespace SportsNotes.Interfaces
{
    public interface IExerciseService
    {
        IEnumerable<ProgressRecordDTO> GetAllExercises();
        ProgressRecordDTO GetExerciseById(int id);
        ProgressRecordDTO AddExercise(ProgressRecordDTO exercise);
        ProgressRecordDTO EditExercise(int id, ProgressRecordDTO exercise);
        void DeleteExercise(int id);
    }
}

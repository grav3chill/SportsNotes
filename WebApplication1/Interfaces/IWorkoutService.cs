using SportsNotes.DTOs;

namespace SportsNotes.Interfaces
{
    public interface IWorkoutService
    {
        IEnumerable<ProgressRecordDTO> GetAllWorkouts();
        ProgressRecordDTO GetWorkoutById(int id);
        ProgressRecordDTO AddWorkout(ProgressRecordDTO workoutDTO);
        ProgressRecordDTO EditWorkout(int id, ProgressRecordDTO workoutDTO);
        void DeleteWorkout(int id);
    }
}

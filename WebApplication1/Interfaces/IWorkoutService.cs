using SportsNotes.DTOs;

namespace SportsNotes.Interfaces
{
    public interface IWorkoutService
    {
        IEnumerable<WorkoutDTO> GetAllWorkouts();
        WorkoutDTO GetWorkoutById(int id);
        WorkoutDTO AddWorkout(WorkoutDTO workoutDTO);
        WorkoutDTO EditWorkout(int id, WorkoutDTO workoutDTO);
        void DeleteWorkout(int id);
    }
}

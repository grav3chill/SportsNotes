using SportsNotes.DTOs;

namespace SportsNotes.Interfaces
{
    public interface IExerciseService
    {
        IEnumerable<ExerciseDTO> GetAllExercises();
        ExerciseDTO GetExerciseById(int id);
        ExerciseDTO AddExercise(ExerciseDTO exercise);
        ExerciseDTO EditExercise(int id, ExerciseDTO exercise);
        void DeleteExercise(int id);
    }
}

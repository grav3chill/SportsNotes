using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsNotes.Database;
using SportsNotes.DTOs;
using SportsNotes.Entities;
using SportsNotes.Interfaces;
namespace SportsNotes.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly SportsNotesDbContext _dbContext;
        private readonly IMapper _mapper;


        public WorkoutService(SportsNotesDbContext dbContext, IMapper mapper)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
        }

        public IEnumerable<WorkoutDTO> GetAllWorkouts()
        {
            var workouts = _dbContext.Workouts.AsNoTracking().ToList();
            var workoutDTOs = _mapper.Map<List<WorkoutDTO>>(workouts);
            return workoutDTOs;
        }

        public WorkoutDTO GetWorkoutById(int id)
        {
            var workoutDTO = _mapper.Map<WorkoutDTO>(_dbContext.Workouts.AsNoTracking().FirstOrDefault(w => w.Id == id));
            return workoutDTO;
        }

        public WorkoutDTO AddWorkout(WorkoutDTO workoutDTO)
        {
            if (workoutDTO == null)
                throw new ArgumentNullException(nameof(workoutDTO));

            var workout = _mapper.Map<Workout>(workoutDTO);
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();

            return _mapper.Map<WorkoutDTO>(workout);
        }

        public WorkoutDTO EditWorkout(int id, WorkoutDTO workoutDTO) 
        {
            if (workoutDTO == null)
                throw new ArgumentNullException(nameof(workoutDTO));

            var workoutToEditDTO = _dbContext.Workouts.FirstOrDefault(w => w.Id == id); 
            if (workoutToEditDTO == null)
                throw new KeyNotFoundException($"Тренировка с ID {id} не найдена");
            
            _mapper.Map(workoutDTO, workoutToEditDTO);
            _dbContext.SaveChanges();

            return _mapper.Map<WorkoutDTO>(workoutToEditDTO);
        }

        public void DeleteWorkout(int id)
        {
            var workout = _dbContext.Workouts.Find(id);
            if (workout == null)
                throw new KeyNotFoundException($"Тренировка с ID {id} не найдена");

            _dbContext.Workouts.Remove(workout);
            _dbContext.SaveChanges();

        }
    }
}

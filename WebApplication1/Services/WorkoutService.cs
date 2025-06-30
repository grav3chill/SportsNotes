using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsNotes.Database;
using SportsNotes.DTOs;
using SportsNotes.Entities;
using SportsNotes.Interfaces;
using System.Globalization;

namespace SportsNotes.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly SportsNotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public WorkoutService(SportsNotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<WorkoutDTO> GetAllWorkouts()
        {
            var workouts = _dbContext.Workouts.AsNoTracking().ToList();
            return _mapper.Map<List<WorkoutDTO>>(workouts);
        }

        public WorkoutDTO GetWorkoutById(int id)
        {
            var workout = _dbContext.Workouts.AsNoTracking().FirstOrDefault(w => w.Id == id);
            return _mapper.Map<WorkoutDTO>(workout);
        }

        public WorkoutDTO AddWorkout(WorkoutDTO workoutDTO)
        {
            if (workoutDTO == null)
                throw new ArgumentNullException(nameof(workoutDTO));

            ValidateWorkout(workoutDTO);

            var workout = _mapper.Map<Workout>(workoutDTO);
            _dbContext.Workouts.Add(workout);
            _dbContext.SaveChanges();

            return _mapper.Map<WorkoutDTO>(workout);
        }

        public WorkoutDTO EditWorkout(int id, WorkoutDTO workoutDTO)
        {
            if (workoutDTO == null)
                throw new ArgumentNullException(nameof(workoutDTO));

            ValidateWorkout(workoutDTO);

            var workoutToEdit = _dbContext.Workouts.FirstOrDefault(w => w.Id == id);
            if (workoutToEdit == null)
                throw new KeyNotFoundException($"Тренировка с ID {id} не найдена");

            _mapper.Map(workoutDTO, workoutToEdit);
            _dbContext.SaveChanges();

            return _mapper.Map<WorkoutDTO>(workoutToEdit);
        }

        public void DeleteWorkout(int id)
        {
            var workout = _dbContext.Workouts.Find(id);
            if (workout == null)
                throw new KeyNotFoundException($"Тренировка с ID {id} не найдена");

            _dbContext.Workouts.Remove(workout);
            _dbContext.SaveChanges();
        }

        
        private void ValidateWorkout(WorkoutDTO workoutDTO)
        {
            if (string.IsNullOrWhiteSpace(workoutDTO.Type))
                throw new ArgumentException("Тип тренировки обязателен");

            if (workoutDTO.Type.Length > 100)
                throw new ArgumentException("Тип тренировки слишком длинный");

            if (workoutDTO.BurnedCcal < 0)
                throw new ArgumentException("Количество сожжённых калорий не может быть отрицательным");

            if (workoutDTO.BurnedCcal > 2000)
                throw new ArgumentException("Слишком большое значение калорий — введите реальное число");

            if (workoutDTO.Date > DateTime.Now.AddDays(1))
                throw new ArgumentException("Дата тренировки не может быть в будущем");

            if (workoutDTO.Date.Year < 2000)
                throw new ArgumentException("Дата тренировки слишком старая");
        }
    }
}

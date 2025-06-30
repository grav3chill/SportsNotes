using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsNotes.Database;
using SportsNotes.DTOs;
using SportsNotes.Entities;
using SportsNotes.Interfaces;

namespace SportsNotes.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly SportsNotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public ExerciseService(SportsNotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ExerciseDTO> GetAllExercises()
        {
            var exercises = _dbContext.Exercises.AsNoTracking().ToList();
            return _mapper.Map<List<ExerciseDTO>>(exercises);
        }

        public ExerciseDTO GetExerciseById(int id)
        {
            var exercise = _dbContext.Exercises.AsNoTracking().FirstOrDefault(e => e.Id == id);
            return _mapper.Map<ExerciseDTO>(exercise);
        }

        public ExerciseDTO AddExercise(ExerciseDTO exerciseDTO)
        {
            if (exerciseDTO == null)
                throw new ArgumentNullException(nameof(exerciseDTO));

            ValidateExercise(exerciseDTO);

            var exercise = _mapper.Map<Exercise>(exerciseDTO);
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            return _mapper.Map<ExerciseDTO>(exercise);
        }

        public ExerciseDTO EditExercise(int id, ExerciseDTO exerciseDTO)
        {
            if (exerciseDTO == null)
                throw new ArgumentNullException(nameof(exerciseDTO));

            ValidateExercise(exerciseDTO);

            var exerciseToEdit = _dbContext.Exercises.FirstOrDefault(e => e.Id == id);
            if (exerciseToEdit == null)
                throw new KeyNotFoundException($"Упражнение с ID {id} не найдено");

            _mapper.Map(exerciseDTO, exerciseToEdit);
            _dbContext.SaveChanges();

            return _mapper.Map<ExerciseDTO>(exerciseToEdit);
        }

        public void DeleteExercise(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Упражнение с ID {id} не найдено");

            _dbContext.Exercises.Remove(exercise);
            _dbContext.SaveChanges();
        }

       
        private void ValidateExercise(ExerciseDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Название упражнения обязательно");

            if (dto.Name.Length > 100)
                throw new ArgumentException("Название упражнения слишком длинное");

            if (dto.Reps < 0 || dto.Reps > 1000)
                throw new ArgumentException("Неверное количество повторений");

            if (dto.Weight < 0 || dto.Weight > 1000)
                throw new ArgumentException("Неверный вес");

            if (dto.Sets < 1 || dto.Sets > 50)
                throw new ArgumentException("Неверное количество подходов");

        }
    }
}

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
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
        }
        public IEnumerable<ProgressRecordDTO> GetAllExercises()
        {
            var exercises = _dbContext.Workouts.AsNoTracking().ToList();
            var exerciseDTOs = _mapper.Map<List<ProgressRecordDTO>>(exercises);
            return exerciseDTOs;
        }

        public ProgressRecordDTO GetExerciseById(int id)
        {
            var exerciseDTO = _mapper.Map<ProgressRecordDTO>(_dbContext.Exercises.AsNoTracking().FirstOrDefault(w => w.Id == id));
            return exerciseDTO;
        }

        public ProgressRecordDTO AddExercise(ProgressRecordDTO exerciseDTO)
        {
            if (exerciseDTO == null)
                throw new ArgumentNullException(nameof(exerciseDTO));

            var exercise = _mapper.Map<Exercise>(exerciseDTO);
            _dbContext.Exercises.Add(exercise);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(exercise);
        }

        public ProgressRecordDTO EditExercise(int id, ProgressRecordDTO exerciseDTO)
        {
            if (exerciseDTO == null)
                throw new ArgumentNullException(nameof(exerciseDTO));

            var exerciseToEditDTO = _dbContext.Exercises.FirstOrDefault(w => w.Id == id);
            if (exerciseToEditDTO == null)
                throw new KeyNotFoundException($"Упажнение с ID {id} не найдено");

            _mapper.Map(exerciseDTO, exerciseToEditDTO);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(exerciseToEditDTO);
        }

        public void DeleteExercise(int id)
        {
            var exercise = _dbContext.Exercises.Find(id);
            if (exercise == null)
                throw new KeyNotFoundException($"Упажнение с ID {id} не найдено");

            _dbContext.Exercises.Remove(exercise);
            _dbContext.SaveChanges();

        }
    }
}

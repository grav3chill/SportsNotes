using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SportsNotes.Database;
using SportsNotes.DTOs;
using SportsNotes.Entities;
using SportsNotes.Interfaces;

namespace SportsNotes.Services
{
    public class ProgressRecordService : IProgressRecordService
    {
        private readonly SportsNotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProgressRecordService(SportsNotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IEnumerable<ProgressRecordDTO> GetAllProgressRecords()
        {
            var records = _dbContext.ProgressRecords.AsNoTracking().ToList();
            return _mapper.Map<List<ProgressRecordDTO>>(records);
        }

        public ProgressRecordDTO GetProgressRecordById(int id)
        {
            var record = _dbContext.ProgressRecords.AsNoTracking().FirstOrDefault(w => w.Id == id);
            return _mapper.Map<ProgressRecordDTO>(record);
        }

        public ProgressRecordDTO AddProgressRecord(ProgressRecordDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateProgressRecord(dto);

            var entity = _mapper.Map<ProgressRecord>(dto);
            _dbContext.ProgressRecords.Add(entity);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(entity);
        }

        public ProgressRecordDTO EditProgressRecord(int id, ProgressRecordDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ValidateProgressRecord(dto);

            var entity = _dbContext.ProgressRecords.FirstOrDefault(w => w.Id == id);
            if (entity == null)
                throw new KeyNotFoundException($"Прогресс с ID {id} не найден");

            _mapper.Map(dto, entity);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(entity);
        }

        public void DeleteProgressRecord(int id)
        {
            var entity = _dbContext.ProgressRecords.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Прогресс с ID {id} не найден");

            _dbContext.ProgressRecords.Remove(entity);
            _dbContext.SaveChanges();
        }

        private void ValidateProgressRecord(ProgressRecordDTO dto)
        {
            if (dto.WeightProgress < 0 || dto.WeightProgress > 300)
                throw new ArgumentOutOfRangeException(nameof(dto.WeightProgress), "Вес должен быть от 0 до 300");

            if (string.IsNullOrWhiteSpace(dto.Overall))
                throw new ArgumentException("Поле 'Overall' обязательно");

            if (dto.Overall.Length > 100)
                throw new ArgumentException("Поле 'Overall' не должно превышать 100 символов");
        }
    }
}

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
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;
        }

        public IEnumerable<ProgressRecordDTO> GetAllProgressRecords()
        {
            var progressRecords = _dbContext.ProgressRecords.AsNoTracking().ToList();
            var ProgressRecordsDTOs = _mapper.Map<List<ProgressRecordDTO>>(progressRecords);
            return ProgressRecordsDTOs;
        }

        public ProgressRecordDTO GetProgressRecordById(int id)
        {
            var ProgressRecordDTO = _mapper.Map<ProgressRecordDTO>(_dbContext.ProgressRecords.AsNoTracking().FirstOrDefault(w => w.Id == id));
            return ProgressRecordDTO;
        }

        public ProgressRecordDTO AddProgressRecord(ProgressRecordDTO progressRecordDTO)
        {
            if (progressRecordDTO == null)
                throw new ArgumentNullException(nameof(progressRecordDTO));

            var progressRecord = _mapper.Map<ProgressRecord>(progressRecordDTO);
            _dbContext.ProgressRecords.Add(progressRecord);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(progressRecord);
        }

        public ProgressRecordDTO EditProgressRecord(int id, ProgressRecordDTO ProgressRecordDTO)
        {
            if (ProgressRecordDTO == null)
                throw new ArgumentNullException(nameof(ProgressRecordDTO));

            var progressRecordToEditDTO = _dbContext.ProgressRecords.FirstOrDefault(w => w.Id == id);
            if (progressRecordToEditDTO == null)
                throw new KeyNotFoundException($"Прогресс с ID {id} не найден");

            _mapper.Map(ProgressRecordDTO, progressRecordToEditDTO);
            _dbContext.SaveChanges();

            return _mapper.Map<ProgressRecordDTO>(progressRecordToEditDTO);
        }

        public void DeleteProgressRecord(int id)
        {
            var ProgressRecord = _dbContext.ProgressRecords.Find(id);
            if (ProgressRecord == null)
                throw new KeyNotFoundException($"Прогресс с ID {id} не найден");

            _dbContext.ProgressRecords.Remove(ProgressRecord);
            _dbContext.SaveChanges();

        }
    }
}

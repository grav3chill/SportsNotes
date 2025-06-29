using Microsoft.AspNetCore.Mvc;
using SportsNotes.DTOs;
using SportsNotes.Interfaces;

namespace SportsNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressRecordController : ControllerBase
    {
        private readonly IProgressRecordService _progressRecordService;
        private readonly ILogger<ProgressRecordController> _logger;

        public ProgressRecordController(IProgressRecordService progressRecordService, ILogger<ProgressRecordController> logger)
        {
            _progressRecordService = progressRecordService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProgressRecordDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var records = _progressRecordService.GetAllProgressRecords();
                return Ok(records);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка записей прогресса");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProgressRecordDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                var record = _progressRecordService.GetProgressRecordById(id);
                if (record == null)
                    return NotFound($"Запись прогресса с ID {id} не найдена");

                return Ok(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении записи прогресса с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProgressRecordDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ProgressRecordDTO progressRecordDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdRecord = _progressRecordService.AddProgressRecord(progressRecordDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdRecord.Id }, createdRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании записи прогресса");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] ProgressRecordDTO progressRecordDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _progressRecordService.EditProgressRecord(id, progressRecordDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении записи прогресса с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                _progressRecordService.DeleteProgressRecord(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении записи прогресса с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}

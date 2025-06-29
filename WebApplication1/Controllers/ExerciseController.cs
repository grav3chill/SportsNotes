using Microsoft.AspNetCore.Mvc;
using SportsNotes.DTOs;
using SportsNotes.Interfaces;

namespace SportsNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IExerciseService exerciseService, ILogger<ExerciseController> logger)
        {
            _exerciseService = exerciseService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ExerciseDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var exercises = _exerciseService.GetAllExercises();
                return Ok(exercises);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка упражнений");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExerciseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                var exercise = _exerciseService.GetExerciseById(id);
                if (exercise == null)
                    return NotFound($"Упражнение с ID {id} не найдено");

                return Ok(exercise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении упражнения с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ExerciseDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] ExerciseDTO exerciseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdExercise = _exerciseService.AddExercise(exerciseDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdExercise.Id }, createdExercise);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании упражнения");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] ExerciseDTO exerciseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _exerciseService.EditExercise(id, exerciseDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении упражнения с ID {id}");
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
                _exerciseService.DeleteExercise(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении упражнения с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}

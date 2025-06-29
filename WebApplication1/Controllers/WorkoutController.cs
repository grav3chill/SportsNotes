using Microsoft.AspNetCore.Mvc;
using SportsNotes.DTOs;
using SportsNotes.Interfaces;

namespace SportsNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly ILogger<WorkoutsController> _logger;

        public WorkoutsController(
            IWorkoutService workoutService,
            ILogger<WorkoutsController> logger)
        {
            _workoutService = workoutService;
            _logger = logger;
        }

        /// <summary>
        /// Получить все тренировки
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkoutDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var workouts = _workoutService.GetAllWorkouts();
                return Ok(workouts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка тренировок");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Получить тренировку по ID
        /// </summary>
        /// <param name="id">ID тренировки</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkoutDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetById(int id)
        {
            try
            {
                var workout = _workoutService.GetWorkoutById(id);

                if (workout == null)
                    return NotFound($"Тренировка с ID {id} не найдена");

                return Ok(workout);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении тренировки с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Создать новую тренировку
        /// </summary>
        /// <param name="workoutDTO">Данные тренировки</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkoutDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Create([FromBody] WorkoutDTO workoutDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdWorkout = _workoutService.AddWorkout(workoutDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdWorkout.Id }, createdWorkout);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании тренировки");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Обновить тренировку
        /// </summary>
        /// <param name="id">ID тренировки</param>
        /// <param name="workoutDTO">Обновленные данные</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(int id, [FromBody] WorkoutDTO workoutDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _workoutService.EditWorkout(id, workoutDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении тренировки с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        /// <summary>
        /// Удалить тренировку
        /// </summary>
        /// <param name="id">ID тренировки</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            try
            {
                _workoutService.DeleteWorkout(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении тренировки с ID {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}
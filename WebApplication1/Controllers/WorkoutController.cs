// SportsNotes/Controllers/WorkoutsController.cs
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

        
        // Получить все тренировки
        
        [HttpGet]
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

        
        // Получить тренировку по ID
        
        [HttpGet("{id}")]
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

        
        // Создать новую тренировку
        
        [HttpPost]
        public IActionResult Create([FromBody] WorkoutDTO workoutDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdWorkout = _workoutService.AddWorkout(workoutDTO);
                return StatusCode(201, createdWorkout);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании тренировки");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        
        // Обновить тренировку
        
        [HttpPut("{id}")]
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

        
        // Удалить тренировку
        
        [HttpDelete("{id}")]
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

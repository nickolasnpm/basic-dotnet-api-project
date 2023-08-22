using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using UdemyProject.Models.Domain;
using UdemyProject.Models.DTO;
using UdemyProject.Repositories;

namespace UdemyProject.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class DifficultyController : Controller
    {
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly ILogger<DifficultyController> _logger;
        public DifficultyController(IDifficultyRepository difficultyRepository, ILogger<DifficultyController> logger)
        {
            _difficultyRepository = difficultyRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetAllDifficultyAsync()
        {
            _logger.LogInformation("GetAllDifficultyAsync method is executed");

            try
            {
                IEnumerable<DifficultyDomain> difficultyDomain = await _difficultyRepository.GetAllAsync();

                _logger.LogInformation($"Requested data from GetAllDifficultyAsync method: {JsonSerializer.Serialize(difficultyDomain)}");

                return Ok(difficultyDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetDifficultyAsync(Guid id)
        {
            _logger.LogInformation("GetDifficultyAsync method is executed");

            try
            {
                DifficultyDomain? difficultyDomain = await _difficultyRepository.GetAsync(id);

                if (difficultyDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation($"Requested data from GetDifficultyAsync method: {JsonSerializer.Serialize(difficultyDomain)}");

                return Ok(difficultyDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddDifficultyAsync (DifficultyDTO difficultyDTO)
        {
            _logger.LogInformation("AddDifficultyAsync method is executed");

            try
            {

                DifficultyDomain? difficultyDomain = new DifficultyDomain
                {
                    Code = difficultyDTO.Code,
                };

                difficultyDomain = await _difficultyRepository.AddAsync(difficultyDomain);
                
                _logger.LogInformation($"The object is added: {JsonSerializer.Serialize(difficultyDomain)}");

                return Ok(difficultyDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateDifficultyAsync(Guid id, DifficultyDTO difficultyDTO)
        {
            _logger.LogInformation("UpdateDifficultyAsync method is executed");

            try
            {
                DifficultyDomain? difficultyDomain = new DifficultyDomain
                {
                    Code = difficultyDTO.Code
                };

                difficultyDomain = await _difficultyRepository.UpdateAsync(id, difficultyDomain);

                if (difficultyDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation($"The object is updated: {JsonSerializer.Serialize(difficultyDomain)}");

                return Ok(difficultyDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpDelete] 
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteDifficultyAsync(Guid id)
        {
            _logger.LogInformation("DeleteDifficultyAsync method is executed");

            try
            {
                DifficultyDomain? difficultyDomain = await _difficultyRepository.DeleteAsync(id);

                if (difficultyDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation("The object is deleted");

                return Ok(difficultyDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }
    }
}

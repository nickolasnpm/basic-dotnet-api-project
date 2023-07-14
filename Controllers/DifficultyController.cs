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
        public DifficultyController(IDifficultyRepository difficultyRepository)
        {
            _difficultyRepository = difficultyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetAllDifficulty()
        {
            try
            {
                IEnumerable<DifficultyDomain> difficulty = await _difficultyRepository.GetAllAsync();
                return Ok(difficulty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetDifficultyAsync(Guid id)
        {
            try
            {
                DifficultyDomain? difficulty = await _difficultyRepository.GetAsync(id);

                if (difficulty == null)
                {
                    return NotFound();
                }

                return Ok(difficulty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddDifficulty(DifficultyDTO difficultyDTO)
        {
            try
            {

                DifficultyDomain? difficulty = new DifficultyDomain
                {
                    Code = difficultyDTO.Code,
                };

                difficulty = await _difficultyRepository.AddAsync(difficulty);
                return Ok(difficulty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateDifficulty(Guid id, DifficultyDTO difficultyDTO)
        {
            try
            {
                DifficultyDomain? difficulty = new DifficultyDomain
                {
                    Code = difficultyDTO.Code
                };

                difficulty = await _difficultyRepository.UpdateAsync(id, difficulty);

                if (difficulty == null)
                {
                    return NotFound();
                }

                return Ok(difficulty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete] 
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkDifficulty(Guid id)
        {
            try
            {
                DifficultyDomain? difficulty = await _difficultyRepository.DeleteAsync(id);

                if (difficulty == null)
                {
                    return NotFound();
                }

                return Ok(difficulty);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

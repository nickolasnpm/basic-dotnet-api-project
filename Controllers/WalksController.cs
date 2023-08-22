using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyProject.Models.Domain;
using UdemyProject.Repositories;

namespace UdemyProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IRegionRepository _regionRepository; 
        private readonly IDifficultyRepository _difficultyRepository;
        private readonly ILogger<WalksController> _logger;
        public WalksController(IWalkRepository walkRepository, IRegionRepository regionRepository, IDifficultyRepository difficultyRepository, ILogger<WalksController> logger)
        {
            _walkRepository = walkRepository;
            _regionRepository = regionRepository;
            _difficultyRepository = difficultyRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            _logger.LogInformation("GetAllWalksAsync method is invoked");

            try
            {
                IEnumerable<WalkDomain> walkDomain = await _walkRepository.GetAllAsync();

                _logger.LogInformation($"Requested data from GetAllWalksAsync method: {JsonSerializer.Serialize(walkDomain)}");

                return Ok(walkDomain);
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
        public async Task<IActionResult> GetWalksAsync(Guid id)
        {
            _logger.LogInformation("GetWalksAsync method is invoked");

            try
            {
                WalkDomain? walkDomain = await _walkRepository.GetAsync(id);

                if (walkDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation($"Requested data from GetWalksAsync method: {JsonSerializer.Serialize(walkDomain)}");

                return Ok(walkDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.WalkDTO walkDTO)
        {
            _logger.LogInformation("AddWalkAsync method is invoked");

            try
            {
                if (!await ValidateAddWalkAsync(walkDTO))
                {
                    return BadRequest(ModelState);
                }

                WalkDomain? walkDomain = new WalkDomain
                {
                    Name = walkDTO.Name,
                    Length = walkDTO.Length,
                    RegionId = walkDTO.RegionId,
                    DifficultyId = walkDTO.DifficultyId,
                };

                walkDomain = await _walkRepository.AddAsync(walkDomain);

                _logger.LogInformation($"The object is added: {JsonSerializer.Serialize(walkDomain)}");

                return Ok(walkDomain);
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
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.WalkDTO walkDTO)
        {
            _logger.LogInformation("UpdateWalkAsync method is invoked");

            try
            {
                if (!await ValidateUpdateWalkAsync(walkDTO))
                {
                    return BadRequest(ModelState);
                }

                WalkDomain? walkDomain = new WalkDomain
                {
                    Name = walkDTO.Name,
                    Length = walkDTO.Length,
                    RegionId = walkDTO.RegionId,
                    DifficultyId = walkDTO.DifficultyId,
                };

                walkDomain = await _walkRepository.UpdateAsync(id, walkDomain);

                if (walkDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation($"The object is updated: {JsonSerializer.Serialize(walkDomain)}");

                return Ok(walkDomain);
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
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            _logger.LogInformation("DeleteWalkAsync method is invoked");

            try
            {
                WalkDomain? walkDomain = await _walkRepository.DeleteAsync(id);

                if (walkDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation("The object is deleted");

                return Ok(walkDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.ToString());
                return BadRequest(e.Message);
            }
            
        }

        #region Private : Data Validation

        private async Task<bool> ValidateAddWalkAsync (Models.DTO.WalkDTO walkDTO)
        {    

            RegionDomain? region = await _regionRepository.GetAsync(walkDTO.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(walkDTO.RegionId), $"{nameof(walkDTO.RegionId)} is Invalid");
            }

            DifficultyDomain? difficulty = await _difficultyRepository.GetAsync(walkDTO.DifficultyId);

            if (difficulty == null)
            {
                ModelState.AddModelError(nameof(walkDTO.DifficultyId), $"{nameof(walkDTO.DifficultyId)} is Invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> ValidateUpdateWalkAsync (Models.DTO.WalkDTO walkDTO)
        {

            RegionDomain? region = await _regionRepository.GetAsync(walkDTO.RegionId);

            if (region == null)
            {
                ModelState.AddModelError(nameof(walkDTO.RegionId), $"{nameof(walkDTO.RegionId)} is Invalid");
            }

            DifficultyDomain? difficulty = await _difficultyRepository.GetAsync(walkDTO.DifficultyId);

            if (difficulty == null)
            {
                ModelState.AddModelError(nameof(walkDTO.DifficultyId), $"{nameof(walkDTO.DifficultyId)} is Invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
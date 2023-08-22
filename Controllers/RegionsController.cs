using Microsoft.AspNetCore.Mvc;
using UdemyProject.Models.Domain;
using UdemyProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace UdemyProject.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(IRegionRepository regionRepository, ILogger<RegionsController> logger)
        {
            _regionRepository = regionRepository;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetAllRegionsAsync()
        {

            _logger.LogInformation("GetAllRegionsAsync method is invoked");

            try
            {
                IEnumerable<RegionDomain> regionDomain = await _regionRepository.GetAllAsync();
                
                _logger.LogInformation($"Requested data from GetAllRegionsAsync method: {JsonSerializer.Serialize(regionDomain)}");
                
                return Ok(regionDomain);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:guid}")] 
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {

            _logger.LogInformation("GetRegionAsync method is invoked");

            try
            {
                RegionDomain? regionDomain = await _regionRepository.GetAsync(id);
                
                if (regionDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }
                
                _logger.LogInformation($"Requested data from GetRegionAsync method: {JsonSerializer.Serialize(regionDomain)}");

                return Ok(regionDomain);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                return BadRequest(e.Message);
            }   
        }

        [HttpPost]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> AddRegionAsync(Models.DTO.RegionDTO regionDTO)
        {
            _logger.LogInformation("AddRegionAsync method is invoked");

            try
            {
                RegionDomain? regionDomain = new RegionDomain()
                {
                    Code = regionDTO.Code,
                    Area = regionDTO.Area,
                    Lat = regionDTO.Lat,
                    Long = regionDTO.Long,
                    Name = regionDTO.Name,
                    Population = regionDTO.Population
                }; 

                regionDomain = await _regionRepository.AddAsync(regionDomain);

                _logger.LogInformation($"The following data is added to the system: {JsonSerializer.Serialize(regionDomain)}");

                return Ok(regionDomain);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.RegionDTO regionDTO)
        {
            _logger.LogInformation("UpdateRegionAsync method is invoked");

            try
            {
                 RegionDomain? regionDomain = new RegionDomain()
                {
                    Code = regionDTO.Code,
                    Area = regionDTO.Area,
                    Lat = regionDTO.Lat,
                    Long = regionDTO.Long,
                    Name = regionDTO.Name,
                    Population = regionDTO.Population,
                };

                regionDomain = await _regionRepository.UpdateAsync(id, regionDomain);

                if (regionDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation($"The following data in the system is updated: {JsonSerializer.Serialize(regionDomain)}");

                return Ok(regionDomain);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            _logger.LogInformation("DeleteRegionAsync method is invoked");

            try
            {
                RegionDomain? regionDomain = await _regionRepository.DeleteAsync(id);

                if (regionDomain == null)
                {
                    _logger.LogError("Object is not in the system!");
                    return NotFound();
                }

                _logger.LogInformation("The object is deleted");

                return Ok(regionDomain);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using UdemyProject.Models.Domain;
using UdemyProject.Repositories;
using Microsoft.AspNetCore.Authorization;
  
namespace UdemyProject.Controllers
{
    [ApiController]
    [Route("[controller]")] 
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            try
            {
                IEnumerable<RegionDomain> regionDomain = await _regionRepository.GetAllAsync();
                return Ok(regionDomain);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("{id:guid}")] 
        [Authorize(Roles = "reader,writer")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            try
            {
                RegionDomain? regionDomain = await _regionRepository.GetAsync(id);
                
                if (regionDomain == null)
                {
                    return NotFound();
                }

                return Ok(regionDomain);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }   
        }

        [HttpPost]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> AddRegionAsync(Models.DTO.RegionDTO regionDTO)
        {

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
                return Ok(regionDomain);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.RegionDTO regionDTO)
        {
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
                    return NotFound();
                }

                return Ok(regionDomain);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")] 
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            try
            {
                RegionDomain? regionDomain = await _regionRepository.DeleteAsync(id);

                if (regionDomain == null)
                {
                    return NotFound();
                }

                return Ok(regionDomain);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

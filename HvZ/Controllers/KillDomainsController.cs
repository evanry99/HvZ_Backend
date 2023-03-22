using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.KillDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class KillDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IKillService _killService;

        public KillDomainsController(IMapper mapper, IKillService killService)
        {
            _mapper = mapper;
            _killService = killService;
        }

        /// <summary>
        /// Get all kills in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of all kills </response>
        /// <response code="500"> Internal error </response>
        // GET: api/KillDomains
        [HttpGet("{gameId}/kill")]
        public async Task<ActionResult<IEnumerable<KillReadDTO>>> GetKills(int gameId)
        {
            return _mapper.Map<List<KillReadDTO>>(await _killService.GetAllKillsAsync(gameId));
        }

        /// <summary>
        /// Get a kill by game Id and kill Id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific kill </response>
        /// <response code="404"> KIll not found </response> 
        /// <response code="500"> Internal error </response> 
        // GET: api/KillDomains/5
        [HttpGet("{gameId}/kill/{killId}")]
        public async Task<ActionResult<KillReadDTO>> GetKillDomain(int gameId, int killId)
        {
            var killDomain = await _killService.GetKillAsync(gameId, killId);

            if (killDomain == null)
            {
                return NotFound();
            }

            return _mapper.Map<KillReadDTO>(killDomain);
        }

        /// <summary>
        /// Update a kill by id
        /// </summary>
        /// <param name="killDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. Kill updated  </response>
        /// <response code="404"> Kill not found </response> 
        /// <response code="500"> Internal error </response> 
        // PUT: api/KillDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gameId}/kill/{killId}")]
        public async Task<IActionResult> PutKillDomain(KillEditDTO killDTO, int gameId, int killId)
        {
            if (killId != killDTO.Id)
            {
                return BadRequest();
            }

            if (!_killService.KillExists(killId))
            {
                return NotFound();
            }


            KillDomain killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.UpdateKillAsync(killDomain, gameId, killId);

            return NoContent();
        }

        /// <summary>
        /// Add a new kill
        /// </summary>
        /// <param name="killDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="201"> Kill created successfully </response>
        /// <response code="400"> Bad request </response> 
        /// <response code="500"> Internal error </response> 
        // POST: api/KillDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{gameId}/kill")]
        public async Task<ActionResult<KillReadDTO>> PostKillDomain(KillCreateDTO killDTO, int gameId)
        {
            var killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.AddKillAsync(killDomain, gameId);

            return CreatedAtAction("PostKillDomain", new { id = killDomain.Id }, killDomain);
        }

        /// <summary>
        /// Delete a kill by id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="killId"></param>
        /// <returns></returns>
        /// <response code="204"> Kill deleted successfully </response>
        /// <response code="400"> Bad request </response> 
        /// <response code="404"> Kill not found </response> 
        /// <response code="500"> Internal error </response> 
        // DELETE: api/KillDomains/5
        [HttpDelete("{gameId}/kill/{killId}")]
        public async Task<IActionResult> DeleteKillDomain(int gameId, int killId)
        {
            var killDomain = await _killService.GetKillAsync(gameId, killId);

            if (killDomain == null)
            {
                return NotFound();
            }

            await _killService.DeleteKillAsync(gameId, killId);

            return NoContent();
        }
    }
}

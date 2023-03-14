using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HvZ.Data;
using HvZ.Model.Domain;
using AutoMapper;
using HvZ.Services;
using HvZ.Model.DTO.KillDTO;

namespace HvZ.Controllers
{
    [Route("api/kill")]
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
        /// Get all kills
        /// </summary>
        /// <returns></returns>
        // GET: api/KillDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KillReadDTO>>> GetKills()
        {
            return _mapper.Map<List<KillReadDTO>>(await _killService.GetAllKillsAsync());
        }

        /// <summary>
        /// Get a kill by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/KillDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KillReadDTO>> GetKillDomain(int id)
        {
            var killDomain = await _killService.GetKillAsync(id);

            if (killDomain == null)
            {
                return NotFound();
            }

            return _mapper.Map<KillReadDTO>(killDomain);
        }


        /// <summary>
        /// Update a kill by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="killDTO"></param>
        /// <returns></returns>
        // PUT: api/KillDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKillDomain(int id, KillEditDTO killDTO)
        {
            if (id != killDTO.Id)
            {
                return BadRequest();
            }

            if (!_killService.KillExists(id))
            {
                return NotFound();
            }


            KillDomain killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.UpdateKillAsync(killDomain);

            return NoContent();
        }


        /// <summary>
        /// Add a new kill
        /// </summary>
        /// <param name="killDTO"></param>
        /// <returns></returns>
        // POST: api/KillDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KillReadDTO>> PostKillDomain(KillCreateDTO killDTO)
        {
            var killDomain = _mapper.Map<KillDomain>(killDTO);
            await _killService.AddKillAsync(killDomain);

            return CreatedAtAction("GetKillDomain", new { id = killDomain.Id }, killDomain);
        }


        /// <summary>
        /// Delete a kill by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/KillDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKillDomain(int id)
        {
            if (!_killService.KillExists(id))
            {
                return NotFound();
            }

            await _killService.DeleteKillAsync(id);

            return NoContent();
        }
    }
}

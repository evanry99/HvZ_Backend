﻿using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadCheckInDTO;
using HvZ.Model.DTO.SquadDTO;
using HvZ.Model.DTO.SquadMemberDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("api/game/")]
    [Tags("Squad")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]
    public class SquadDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISquadCheckInService _squadCheckInService;
        private readonly ISquadService _squadService;

        public SquadDomainsController(IMapper mapper, ISquadCheckInService squadCheckInService, ISquadService squadService)
        {
            _mapper = mapper;
            _squadCheckInService = squadCheckInService;
            _squadService = squadService;
        }

        /// <summary>
        /// Get all squads by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of squads in a game</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/squad")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SquadReadDTO>>> GetSquads(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }
            var squadModel = await _squadService.GetAllSquadsAsync(gameId);
            var gameReadDTO = _mapper.Map<List<SquadReadDTO>>(squadModel);
            return Ok(gameReadDTO);
        }

        /// <summary>
        /// Get a specific squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific squad in a game</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game or squad not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/squad/{squadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquadReadDTO>> GetSquadDomain(int gameId, int squadId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!await _squadService.SquadExistsAsync(gameId, squadId)) 
            {
                return NotFound($"Squad with id {squadId} does not exist in game {gameId} ");
            }

            var squadReadDTO = await _squadService.GetSquadAsync(gameId, squadId);

            return _mapper.Map<SquadReadDTO>(squadReadDTO);
        }

        /// <summary>
        /// Update a squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <param name="squadDTO"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. Squad updated</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> The game or squad was not found</response>
        /// <response code="500"> Internal error</response>
        [HttpPut(("{gameId}/squads/{squadId}"))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSquad(int gameId, int squadId, SquadEditDTO squadDTO)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!await _squadService.SquadExistsAsync(gameId, squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist in game {gameId}");
            }

            var squadModel = _mapper.Map<SquadDomain>(squadDTO);
            await _squadService.UpdateSquadAsync(gameId,squadId,squadModel);

            return NoContent();
        }

        /// <summary>
        /// Add a new squad in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadDTO"></param>
        /// <returns></returns>
        /// <response code="201"> Squad created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> The game or player was not found</response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/squad")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquadReadDTO>> PostSquad(int gameId,SquadCreateDTO squadDTO)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var squadDomain = _mapper.Map<SquadDomain>(squadDTO);
            var addedSquad = await _squadService.AddSquadAsync(gameId, squadDomain, squadDTO.SquadMember.PlayerId);
            var squadReadDTO = _mapper.Map<SquadReadDTO>(addedSquad);

            return CreatedAtAction("PostSquad", new { gameId = addedSquad.GameId, squadId = addedSquad.Id }, squadReadDTO);
        }

        /// <summary>
        /// Add a squad member to the specific squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <param name="squadMemberDTO"></param>
        /// <returns></returns>
        /// <response code="201"> Squad member created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> The game or player was not found</response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/squad/{squadId}/join")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquadMemberReadDTO>> PostSquadMember(int gameId, int squadId, SquadMemberCreateDTO squadMemberDTO)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!await _squadService.PlayerExistsAsync(squadMemberDTO.PlayerId))
            {
                return NotFound($"Player with id {squadMemberDTO.PlayerId} does not exist");
            }

            if (!await _squadService.SquadExistsAsync(gameId, squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist in game {gameId}");
            }

            var squadMemberDomain = _mapper.Map<SquadMemberDomain>(squadMemberDTO);
            var addedSquadMember = await _squadService.AddSquadMemberAsync(gameId, squadId, squadMemberDomain, squadMemberDTO.PlayerId);
            var squadMemberReadDTO = _mapper.Map<SquadMemberReadDTO>(addedSquadMember);

            return CreatedAtAction("PostSquadMember", new { gameId = addedSquadMember.GameId, squadId = addedSquadMember.SquadId, squadMemberId = addedSquadMember.Id }, squadMemberReadDTO);
        }

        /// <summary>
        /// Delete a squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        /// <response code="204"> Squad deleted succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game or squad not found</response>
        /// <response code="500"> Internal error</response>
        [HttpDelete("{gameId}/squad/{squadId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSquadDomain(int gameId, int squadId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!await _squadService.SquadExistsAsync(gameId, squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist in game id {gameId}");
            }

            await _squadService.DeleteSquadAsync(gameId, squadId);

            return NoContent();
        }

        /// <summary>
        /// Get all squad check-ins by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of squads check-ins for a squad in a game</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game or squad not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/squad/{squadId}/check-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SquadCheckInReadDTO>>> GetSquadCheckIns(int gameId, int squadId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!_squadCheckInService.SquadExists(squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist");
            }

            var squadCheckInModel = await _squadCheckInService.GetSquadCheckInsAsync(gameId, squadId);

            if(squadCheckInModel== null)
            {
                return NotFound($"Squad with id {squadId} does not exist in game {gameId}");
            }

            return Ok(_mapper.Map<List<SquadCheckInReadDTO>>(squadCheckInModel));
        }

        /// <summary>
        /// Post a new squad check-in by gameId and squadId
        /// </summary>
        /// <param name="squadCheckInDTO"></param>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        /// <response code="201"> Squad check-in created succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game or squad not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/squad/{squadId}/check-in")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SquadCheckInReadDTO>> PostSquadCheckIn(SquadCheckInCreateDTO squadCheckInDTO, int gameId, int squadId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            if (!_squadCheckInService.SquadExists(squadId))
            {
                return NotFound($"Squad with id {squadId} does not exist");
            }
            var squadCheckInModel = await _squadCheckInService.GetSquadCheckInsAsync(gameId, squadId);
            if (squadCheckInModel == null)
            {
                return NotFound($"Squad with id {squadId} does not exist in game {gameId}");
            }

            SquadCheckInDomain squadCheckInDomain = _mapper.Map<SquadCheckInDomain>(squadCheckInDTO);

            await _squadCheckInService.AddSquadCheckInAsync(squadCheckInDomain, gameId, squadId);

            return CreatedAtAction("PostSquadCheckIn", new { id = squadCheckInDomain.Id }, squadCheckInDomain);
        }

        /// <summary>
        /// Get a squad member by gameID and playerId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        /// <response code="200">Get squad member sucessfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404">Squad member not Found</response>
        /// <response code="500">Internal error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{gameId}/squadMember/{playerId}")]
        public async Task<ActionResult<SquadMemberReadDTO>> GetSquadMember(int gameId, int playerId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (playerId <= 0)
            {
                return BadRequest($"Invalid squad id {playerId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var squadMemberModel = await _squadService.GetSquadMemberAsync(gameId, playerId);

            if (squadMemberModel == null)
            {
                return NotFound($"Squad member could not be found with gameId: {gameId} and playerId {playerId}");
            }

            return Ok(_mapper.Map<SquadMemberReadDTO>(squadMemberModel));
        }

        /// <summary>
        /// Get all squad members in a squad by gameId and squadId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        /// <response code="200">Get squad member sucessfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404">Squad member not Found</response>
        /// <response code="500">Internal error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{gameId}/squadMembers/{squadId}")]
        public async Task<ActionResult<IEnumerable<SquadMemberReadDTO>>> GetSquadMembers(int gameId, int squadId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadId <= 0)
            {
                return BadRequest($"Invalid squad id {squadId}. The squadId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var squadMemberModel = await _squadService.GetSquadMembersAsync(gameId, squadId);

            if (squadMemberModel == null)
            {
                return NotFound($"Squad member could not be found with gameId: {gameId} and squadId {squadId}");
            }

            return Ok(_mapper.Map<List<SquadMemberReadDTO>>(squadMemberModel));
        }

        /// <summary>
        /// Method to delete a squad member by gameId and squadMemberId
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="squadMemberId"></param>
        /// <returns></returns>
        /// <response code="204"> Squad member deleted succesfully </response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404">Squad member not Found</response>
        /// <response code="500">Internal error</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{gameId}/squadMember/{squadMemberId}")]
        public async Task<ActionResult> DeleteSquadMember(int gameId, int squadMemberId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (squadMemberId <= 0)
            {
                return BadRequest($"Invalid squad member id {squadMemberId}. The squadMemberId must be greater than zero.");
            }

            if (!await _squadService.GameExistsAsync(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            await _squadService.DeleteSquadMemberAsync(gameId, squadMemberId);

            return NoContent();
        }
    }
}

using AutoMapper;
using HvZ.Model;
using HvZ.Model.Domain;
using HvZ.Model.DTO.ChatDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HvZ.Controllers
{
    [Authorize]
    [Route("api/game")]
    [Tags("Chat")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ChatDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;
        private readonly IHubContext<BroadcastHub> _hubContext;

        public ChatDomainsController(IMapper mapper, IChatService chatService, IHubContext<BroadcastHub> hubContext)
        {
            _mapper = mapper;
            _chatService = chatService;
            _hubContext= hubContext;
        }

        /// <summary>
        /// Create a new chat in a game
        /// </summary>
        /// <param name="chatDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="201"> Chat created successfully</response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found </response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/chat")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ChatReadDTO>> PostChat(ChatCreateDTO chatDTO, int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (!_chatService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var chatDomain = _mapper.Map<ChatDomain>(chatDTO);
            await _chatService.AddChatAsync(chatDomain, gameId);
            await _hubContext.Clients.All.SendAsync("chat", chatDTO);

            return CreatedAtAction("PostChat", new { id = chatDomain.Id }, chatDTO);
        }

        /// <summary>
        /// Delete a chat by chat id
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        /// <response code="204"> Chat deleted successfully</response>
        /// <response code="400"> Bad request </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Chat not found</response>
        /// <response code="500"> Internal error</response>
        [HttpDelete("{gameId}/chat/{chatId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteChatDomain(int gameId, int chatId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid game id {gameId}. The gameId must be greater than zero.");
            }

            if (chatId <= 0)
            {
                return BadRequest($"Invalid chat id {chatId}. The chatId must be greater than zero.");
            }

            if (!_chatService.ChatExists(chatId))
            {
                return NotFound($"Chat with id {chatId} does not exist");
            }

            if(!_chatService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            await _chatService.DeleteChatAsync(gameId, chatId);

            return NoContent();
        }

        /// <summary>
        /// Get all chats in a game
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of all chats in a game</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="401"> Unauthorized </response>
        /// <response code="404"> Game not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/chat")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ChatReadDTO>>> GetGameChats(int gameId)
        {
            if (gameId <= 0)
            {
                return BadRequest($"Invalid gameId parameter id {gameId}. The gameId must be greater than zero.");
            }

            if (!_chatService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var chatModel = await _chatService.GetGameChatsAsync(gameId);

            return _mapper.Map<List<ChatReadDTO>>(chatModel);
        }
    }
}
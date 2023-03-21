using Microsoft.AspNetCore.Mvc;
using HvZ.Data;
using HvZ.Model.Domain;
using AutoMapper;
using HvZ.Services;
using HvZ.Model.DTO.ChatDTO;
using Microsoft.AspNetCore.SignalR;
using HvZ.Model;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
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
        /// Get all chats in a game by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of all chats in a game</response>
        /// <response code="404"> Game not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("{gameId}/chat")]
        public async Task<ActionResult<IEnumerable<ChatReadDTO>>> GetChats(int gameId)
        {
            var chatModel = await _chatService.GetChatsAsync(gameId);

            return _mapper.Map<List<ChatReadDTO>>(chatModel);
        }

        /// <summary>
        /// Create a new chat in a game
        /// </summary>
        /// <param name="chatDTO"></param>
        /// <param name="gameId"></param>
        /// <returns></returns>
        /// <response code="201"> Chat created successfully</response>
        /// <response code="400"> Bad request </response>
        /// <response code="500"> Internal error</response>
        [HttpPost("{gameId}/chat")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ChatReadDTO>> PostChat(ChatCreateDTO chatDTO, int gameId)
        {
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
        /// <param name="chatId"></param>
        /// <returns></returns>
        /// <response code="200"> Chat deleted successfully</response>
        /// <response code="400"> Bad request </response>
        /// <response code="404"> Chat not found</response>
        /// <response code="500"> Internal error</response>
        [HttpDelete("{chatId}/chat")]
        public async Task<IActionResult> DeleteChatDomain(int chatId)
        {
            if (!_chatService.ChatExists(chatId))
            {
                return NotFound($"Chat with id {chatId} does not exist");
            }

            await _chatService.DeleteChatAsync(chatId);

            return NoContent();
        }
    }
}
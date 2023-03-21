using Microsoft.AspNetCore.Mvc;
using HvZ.Data;
using HvZ.Model.Domain;
using AutoMapper;
using HvZ.Services;
using HvZ.Model.DTO.ChatDTO;
using Microsoft.AspNetCore.Authorization;

namespace HvZ.Controllers
{
    [Authorize]
    [Route("api/game")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ChatDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;
      

        public ChatDomainsController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
            
        }

        /// <summary>
        /// Get all chats in a game by game id
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
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
        [HttpPost("{gameId}/chat")]
        public async Task<ActionResult<ChatReadDTO>> PostChat(ChatCreateDTO chatDTO, int gameId)
        {
            if (!_chatService.GameExists(gameId))
            {
                return NotFound($"Game with id {gameId} does not exist");
            }

            var chatDomain = _mapper.Map<ChatDomain>(chatDTO);
            await _chatService.AddChatAsync(chatDomain, gameId);

            return CreatedAtAction("PostChat", new { id = chatDomain.Id }, chatDTO);
        }

        /// <summary>
        /// Delete a chat by chat id
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns></returns>
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
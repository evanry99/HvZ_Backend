using Microsoft.AspNetCore.Mvc;
using HvZ.Data;
using HvZ.Model.Domain;
using AutoMapper;
using HvZ.Services;
using HvZ.Model.DTO.ChatDTO;

namespace HvZ.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class ChatDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatDomainsController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        // GET: api/ChatDomains
        [HttpGet("{gameId}/chat")]
        public async Task<ActionResult<IEnumerable<ChatReadDTO>>> GetChats(int gameId)
        {
            var chatModel = await _chatService.GetChatsAsync(gameId);

            return _mapper.Map<List<ChatReadDTO>>(chatModel);
        }

        // POST: api/ChatDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // DELETE: api/ChatDomains/5
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
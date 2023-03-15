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
using HvZ.Model.DTO.GameDTO;
using HvZ.Model.DTO.ChatDTO;
using HvZ.Model.DTO.PlayerDTO;

namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatDomainsController : ControllerBase
    {
        private readonly HvZDbContext _context;
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatDomainsController(HvZDbContext context, IMapper mapper, IChatService chatService)
        {
            _context = context;
            _mapper = mapper;
            _chatService = chatService;
        }

        // GET: api/ChatDomains
        [HttpGet("{gameId}")]
        public async Task<ActionResult<IEnumerable<ChatReadDTO>>> GetChats(int gameId)
        {
            var chatModel = await _chatService.GetChatsAsync(gameId);

            return _mapper.Map<List<ChatReadDTO>>(chatModel);
        }

        // PUT: api/ChatDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChatDomain(int id, ChatDomain chatDomain)
        {
            if (id != chatDomain.Id)
            {
                return BadRequest();
            }

            _context.Entry(chatDomain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChatDomainExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ChatDomains
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChatDomain>> PostChatDomain(ChatDomain chatDomain)
        {
          if (_context.Chats == null)
          {
              return Problem("Entity set 'HvZDbContext.Chats'  is null.");
          }
            _context.Chats.Add(chatDomain);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChatDomain", new { id = chatDomain.Id }, chatDomain);
        }

        // DELETE: api/ChatDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChatDomain(int id)
        {
            if (_context.Chats == null)
            {
                return NotFound();
            }
            var chatDomain = await _context.Chats.FindAsync(id);
            if (chatDomain == null)
            {
                return NotFound();
            }

            _context.Chats.Remove(chatDomain);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChatDomainExists(int id)
        {
            return (_context.Chats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

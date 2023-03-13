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
using HvZ.Model.DTO.UserDTO;

namespace HvZ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public UserDomainsController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;

        }

        // GET: api/UserDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            var userModel = await _userService.GetAllUserAsync();
            var userReadDTO = _mapper.Map<List<UserReadDTO>>(userModel);
            return userReadDTO;
        }

        // GET: api/UserDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserDomain(int id)
        {
            var userReadDTO = await _userService.GetUserAsync(id);

            if (userReadDTO == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserReadDTO>(userReadDTO);
        }

        // PUT: api/UserDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDomain(int id, UserEditDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }
            var userModel = _mapper.Map<UserDomain>(userDTO);

            await _userService.UpdateUserAsync(userModel);
            return NoContent();



        }

        // POST: api/UserDomains
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> PostUserDomain(UserCreateDTO userDTO)
        {
            var userModel = _mapper.Map<UserDomain>(userDTO);
            await _userService.AddUserAsync(userModel);

            return CreatedAtAction("GetUserDomain", new { id = userModel.Id }, userDTO);
        }


        // DELETE: api/UserDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDomain(int id)
        {
            if (!_userService.UserExists(id))
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return NoContent();

        }
    }
}

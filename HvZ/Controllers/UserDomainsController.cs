using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.UserDTO;
using HvZ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HvZ.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize]

    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserDomainsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;


        public UserDomainsController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;

        }


        /// <summary>
        /// Get all Users
        /// </summary>
        /// <returns></returns>
        /// <response code="200"> Success. Return a list of all users</response>
        /// <response code="500"> Internal error</response>
        // GET: api/UserDomains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetUsers()
        {
            var userModel = await _userService.GetAllUserAsync();
            var userReadDTO = _mapper.Map<List<UserReadDTO>>(userModel);
            return userReadDTO;
        }


        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific user</response>
        /// <response code="404"> User not found. </response>
        /// <response code="500"> Internal error</response>

        // GET: api/UserDomains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadDTO>> GetUserDomain(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest($"Invalid user id {userId}. The gameId must be greater than zero.");
            }
            if (!_userService.UserExists(userId))
            {
                return NotFound($"User with id {userId} does not exsist");
            }
            var userReadDTO = await _userService.GetUserAsync(userId);

            return _mapper.Map<UserReadDTO>(userReadDTO);
        }
        /// <summary>
        /// Get a user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <response code="200"> Success. Return a specific user</response>
        /// <response code="404"> Username not found. </response>
        /// <response code="500"> Internal error</response>
        [HttpGet("username/{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserReadDTO>> GetUserByUsername(string username)
        {

            if (!_userService.UserNameExists(username))
            {
                return NotFound($"User with username: {username} does not exsist");
            }
            var userReadDTO = await _userService.GetUserByUsernameAsync(username);

            return _mapper.Map<UserReadDTO>(userReadDTO);
        }



        /// <summary>
        /// Update a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <response code="204"> Update success. User updated</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> User was not found</response>
        /// <response code="500"> Internal error</response>
        // PUT: api/UserDomains/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDomain(int userId, UserEditDTO userDTO)
        {
            if (userId <= 0)
            {
                return BadRequest($"Invalid user id {userId}. The gameId must be greater than zero.");
            }
            if (!_userService.UserExists(userId))
            {
                return NotFound($"User with id {userId} does not exist");
            }
            var userModel = _mapper.Map<UserDomain>(userDTO);

            await _userService.UpdateUserAsync(userModel);
            return NoContent();



        }


        /// <summary>
        /// Add a new user 
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        /// <response code="201"> User created successfully</response>
        /// <response code="400"> Bad request</response>
        /// <response code="500"> Internal error</response>
        // POST: api/UserDomains
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> PostUserDomain(UserCreateDTO userDTO)
        {
            var userModel = _mapper.Map<UserDomain>(userDTO);
            await _userService.AddUserAsync(userModel);

            return CreatedAtAction("GetUserDomain", new { id = userModel.Id }, userModel);
        }



        /// <summary>
        /// Delete a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204"> User deleted succesfully</response>
        /// <response code="400"> Bad request. </response>
        /// <response code="404"> User not found</response>
        /// <response code="500"> Internal error</response>
        // DELETE: api/UserDomains/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDomain(int userId)
        {
            if (userId <= 0)
            {
                return BadRequest($"Invalid user id {userId}. The gameId must be greater than zero.");
            }

            if (!_userService.UserExists(userId))
            {
                return NotFound($"User with id {userId} does not exist");
            }
            await _userService.DeleteUserAsync(userId);
            return NoContent();

        }
    }
}

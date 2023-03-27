using HvZ.Data;
using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Services
{
    public class UserService : IUserService
    {
        private readonly HvZDbContext _context;

        public UserService(HvZDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to create a new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserDomain> AddUserAsync(UserDomain user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Method to delete a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method to get a list of all users.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserDomain>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Method to get a specific user by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<UserDomain> GetUserAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        /// <summary>
        /// Method to get a specific user by username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<UserDomain> GetUserByUsernameAsync(string username)
        {
            var users = await _context.Users.ToListAsync();
            return users.FirstOrDefault(u => string.Equals(u.UserName, username, StringComparison.Ordinal));
        }

        /// <summary>
        /// Method to edit a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task UpdateUserAsync(UserDomain user, int userId)
        {
            user.Id = userId;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// MEthod to check if user exists.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        /// <summary>
        /// Method to check if username exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UserNameExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}

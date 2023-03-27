using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDomain>> GetAllUserAsync();
        public Task<UserDomain> GetUserAsync(int userId);
        public Task<UserDomain> GetUserByUsernameAsync(string username);
        public Task<UserDomain> AddUserAsync(UserDomain user);
        public Task UpdateUserAsync(UserDomain user, int userId);
        public Task DeleteUserAsync(int userId);
        public bool UserExists(int userId);
        public bool UserNameExists(string username);
    }
}

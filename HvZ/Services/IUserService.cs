using HvZ.Model.Domain;

namespace HvZ.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDomain>> GetAllUserAsync();
        public Task<UserDomain> GetUserAsync(int id);
        public Task<UserDomain> GetUserByUsernameAsync(string username);
        public Task<UserDomain> AddUserAsync(UserDomain user);
        public Task UpdateUserAsync(UserDomain user);
        public Task DeleteUserAsync(int id);
        public bool UserExists(int id);
        public bool UserNameExists(string username);


    }
}

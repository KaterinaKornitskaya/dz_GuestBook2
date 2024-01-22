using dz_GuestBook2.Models;

namespace dz_GuestBook2.Repository
{
    public interface IAccRepository
    {
        IQueryable<User> GetUsersByLogin(LoginModel user);

        Task<User> CreatePassword(User user, RegisterModel model);

        Task<bool> IsLoginExists(string? login);

        Task AddUserToDb(User user);

        Task<List<User>> GetAllUsers();
    }
}

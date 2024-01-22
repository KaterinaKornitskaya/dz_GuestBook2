using dz_GuestBook2.Models;

namespace dz_GuestBook2.Repository
{
    public interface IMesRepository
    {
        Task<List<Messages>> GetAllMessages();

        Task<Messages?> GetMessage(int? id);

        Task CreateMessage(Messages msg, string log);

        Task<bool> MessageExists(int id);

        Task SaveChanges();
    }
}

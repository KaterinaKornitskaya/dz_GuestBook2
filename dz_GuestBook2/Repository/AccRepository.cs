using dz_GuestBook2.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace dz_GuestBook2.Repository
{
    public class AccRepository : IAccRepository
    {
        private readonly GuestBookContext _context;

        public AccRepository(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public IQueryable<User> GetUsersByLogin(LoginModel user) => _context.Users.Where(x => x.Login == user.Login);

        public async Task<User> CreatePassword(User user, RegisterModel regModel)
        {
            await Task.Run(() =>
            {
                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));

                string salt = sb.ToString();
                byte[] password = Encoding.Unicode.GetBytes(salt + regModel.Password);
                byte[] hashPassword = SHA256.HashData(password);

                StringBuilder hash = new(hashPassword.Length);
                for (int i = 0; i < hashPassword.Length; i++)
                    hash.Append(string.Format("{0:X2}", hashPassword[i]));

                user.Password = hash.ToString();
                user.Salt = salt;
            });
            return user;
        }

        public async Task<bool> IsLoginExists(string? login)
        {
            return await _context.Users.AnyAsync(x => x.Login == login);
        }

        public async Task AddUserToDb(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}

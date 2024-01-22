using dz_GuestBook2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace dz_GuestBook2.Repository
{
    public class MesRepository : IMesRepository
    {
        private readonly GuestBookContext _context;

        public MesRepository(GuestBookContext context)
        {
            _context = context;
        }

        public async Task<List<Messages>> GetAllMessages()
        {
            var myContext = _context.Messages.Include(x => x.User);
            
            return await myContext.ToListAsync();
        }

        public async Task<Messages?> GetMessage(int? id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task CreateMessage(Messages msg, string log)
        {
            Messages mes = new Messages();
            mes.Message = msg.Message;
            mes.MessageDate = msg.MessageDate;

            var us = from it in _context.Users
                     where it.Login == log
                     select it;

            msg.User = us.FirstOrDefault();

            await _context.AddAsync(msg);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MessageExists(int id)
        {
            return await _context.Messages.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}

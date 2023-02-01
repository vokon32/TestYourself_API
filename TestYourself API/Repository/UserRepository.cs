using Microsoft.EntityFrameworkCore;
using TestYourself_API.Data;
using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(AppUser user)
        {
            _context.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.TestResults).ToListAsync();
        }

        public async Task<AppUser> GetUserById(string Id)
        {
            return await _context.Users.FindAsync(Id);
        }
        public async Task<AppUser> GetUsersTestsResultById(string Id)
        {
            return await _context.Users.Include(u => u.TestResults).FirstAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }
    }
}

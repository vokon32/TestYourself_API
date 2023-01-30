using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestYourself_API.Data;
using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Test> GetTestAsync(int id)
        {
            return await _context.Tests.Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Test>> GetTestsAsync()
        {
            return await _context.Tests.ToListAsync();
        }

        public bool TestExist(int id)
        {
            return _context.Tests.Any(t => t.Id == id);
        }
    }
}

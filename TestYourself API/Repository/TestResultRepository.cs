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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly ApplicationDbContext _context;

        public TestResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(TestResult test)
        {
            _context.TestResults.Add(test);
            return Save();
        }

        public bool Delete(TestResult test)
        {
            _context.Remove(test);
            return Save();
        }

        public bool TestExist(int id)
        {
            return _context.TestResults.Any(t => t.Id == id);
        }

        public async Task<IEnumerable<TestResult>> GetAll()
        {
            return await _context.TestResults.ToListAsync();
        }

        public async Task<TestResult> GetByIdAsync(int id)
        {
            return await _context.TestResults.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TestResult> GetByIdAsyncNoTracking(int id)
        {
            return await _context.TestResults.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TestResult> GetByTestIdAndUserIdAsNoTracking(int testId, string userId)
        {
            return await _context.TestResults.AsNoTracking().Where(t => t.testId == testId && t.AppUserId == userId).FirstOrDefaultAsync();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(TestResult test)
        {
            _context.Update(test);
            return Save();
        }

        public async Task<IEnumerable<TestResult>> GetAllTestResultsByUserId(string Id)
        {
            return await _context.TestResults.AsNoTracking().Where(t => t.AppUserId == Id).ToListAsync();
        }
    }
}

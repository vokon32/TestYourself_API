
using Microsoft.EntityFrameworkCore;
using TestYourself_API.Data;
using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Question question)
        {
            _context.Add(question);
            return Save();
        }

        public bool Delete(Question question)
        {
            _context.Remove(question);
            return Save();
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Question>> GetByTestId(int id)
        {
            return await _context.Questions.AsNoTracking().Include(q => q.Test).Where(t => t.testId == id).ToListAsync();
        }
        public async Task<Question> GetFirstQuestion(int id)
        {
            return await _context.Questions.Include(q => q.Test).Where(t => t.Test.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Question> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Question question)
        {
            _context.Update(question);
            return Save();
        }
    }
}

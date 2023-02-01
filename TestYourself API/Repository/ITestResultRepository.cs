using TestYourself_API.Models;

namespace TestYourself_API.Repository
{
    public interface ITestResultRepository
    {
        Task<IEnumerable<TestResult>> GetAll();
        Task<TestResult> GetByIdAsync(int id);
        Task<TestResult> GetByIdAsyncNoTracking(int id);
        Task<TestResult> GetByTestIdAndUserIdAsNoTracking(int testId, string userId);
        Task<IEnumerable<TestResult>> GetAllTestResultsByUserId(string Id);
        bool TestExist(int id);
        bool Add(TestResult test);
        bool Update(TestResult test);
        bool Delete(TestResult test);
        bool Save();
    }
}

using Microsoft.EntityFrameworkCore;
using TestYourself_API.Data.Enum;
using TestYourself_API.Data;
using TestYourself_API.Models;
using TestYourself_API.Repository;
using FluentAssertions;

namespace TestYourself_API.Tests.Repository
{
    public class TestRepositoryTests
    {
        private ApplicationDbContext _dbContext;
        private TestRepository _testRepository;

        public TestRepositoryTests()
        {
            _dbContext = GetDbContext();
            _testRepository = new TestRepository(_dbContext);
        }
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            for (int i = 1; i < 11; i++)
            {
                databaseContext.Tests.Add(
                new Test()
                {
                    Title = $"MathTest {i}",
                    questionsAmount = 2,
                    Image = "https://news.harvard.edu/wp-content/uploads/2022/11/iStock-mathproblems-1200x800.jpg",
                    Description = "Really funny test",
                    TestCategory = TestCategory.Math
                });
                databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }

        [Fact]
        public void TestRepository_Add_ReturnsBool()
        {
            var test = new Test()
            {
                Title = "MathTest",
                questionsAmount = 2,
                Image = "https://news.harvard.edu/wp-content/uploads/2022/11/iStock-mathproblems-1200x800.jpg",
                Description = "Really funny test",
                TestCategory = TestCategory.Math
            };


            var result = _testRepository.Add(test);

            result.Should().BeTrue();
        }
        [Fact]
        public async void TestRepository_Update_ReturnsBool()
        {
            var test = await _dbContext.Tests.FirstAsync();
            test.Title = Guid.NewGuid().ToString();


            var result = _testRepository.Update(test);

            result.Should().BeTrue();
        }
        [Fact]
        public async void TestRepository_Delete_ReturnsBool()
        {
            var test = await _dbContext.Tests.FirstAsync();


            var result = _testRepository.Delete(test);

            result.Should().BeTrue();
        }

        [Fact]
        public async void TestRepository_GetByIdAsync_ReturnsTest()
        {
            var id = 1;

            var result = await _testRepository.GetTestAsync(id);

            result.Should().BeOfType<Test>();
            result.Should().NotBeNull();
        }
    }
}

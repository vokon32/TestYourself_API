using Microsoft.EntityFrameworkCore;
using TestYourself_API.Data;
using TestYourself_API.Models;
using TestYourself_API.Repository;
using FluentAssertions;
using FakeItEasy;

namespace TestYourself_API.Tests.Repository
{
    public class QuestionRepositoryTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IQuestionRepository _questionRepository;

        public QuestionRepositoryTests()
        {
            _dbContext = GetDbContext();
            _questionRepository = new QuestionRepository(_dbContext);
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
                databaseContext.Questions.Add(
                new Question()
                {
                    Contain = $"asdasd {i}",
                    FirstAnswer = "asdsad",
                    SecondAnswer = "asdsadas",
                    testId = i
                });
                databaseContext.SaveChangesAsync();
            }

            return databaseContext;
        }
        [Fact]
        public void QuestionRepository_Add_ReturnsBool()
        {
            var question = A.Fake<Question>();


            var result = _questionRepository.Add(question);

            result.Should().BeTrue();
        }
        [Fact]
        public async void QuestionRepository_Update_ReturnsBool()
        {
            var question = await _dbContext.Questions.FirstAsync();
            question.Contain = Guid.NewGuid().ToString();


            var result = _questionRepository.Update(question);

            result.Should().BeTrue();
        }
        [Fact]
        public async void QuestionRepository_Delete_ReturnsBool()
        {
            var question = await _dbContext.Questions.FirstAsync();


            var result = _questionRepository.Delete(question);

            result.Should().BeTrue();
        }

        [Fact]
        public async void QuestionRepository_GetByIdAsync_ReturnsTest()
        {
            var id = 1;

            var result = await _questionRepository.GetByIdAsync(id);

            result.Should().BeOfType<Question>();
            result.Should().NotBeNull();
        }
       
        [Fact]
        public async void QuestionRepository_GetAll_ReturnsIEnumerableTest()
        {

            var result = await _questionRepository.GetAll();

            result.Should().BeOfType<List<Question>>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async void QuestionRepository_GetByTestId_ReturnsListQuestions()
        {
            var testId = 1;

            var result = await _questionRepository.GetByTestId(testId);

            result.Should().BeOfType<List<Question>>();
            result.Should().NotBeNull();
        }

        [Fact]
        public async void QuestionRepository_GetByIdAsyncNoTracking_ReturnsQuestion()
        {
            var Id = 1;

            var result = await _questionRepository.GetByIdAsync(Id);

            result.Should().BeOfType<Question>();
            result.Should().NotBeNull();
        }

    }
}

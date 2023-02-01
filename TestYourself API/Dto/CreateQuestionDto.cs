

using TestYourself_API.Models;

namespace TestYourself_API.Dto
{
    public class CreateQuestionDto
    {
        public int Id { get; set; }
        public string FirstAnswer { get; set; } = null!;
        public string SecondAnswer { get; set; } = null!;
        public string? CorrectAnswer { get; set; }
        public string? ChosenAnswer { get; set; }
        public string Contain { get; set; } = null!;
        public int? CurrentAmountOfQuestions { get; set; }
        public bool isFull { get; set; } = false;
        public int testId { get; set; }
    }
}

using TestYourself_API.Models;

namespace TestYourself_API.Dto
{
    public class FinishQuestionDto
    {
        public bool CanBePassedAgain { get; set; }
        public string ChosenAnswer { get; set; }
        public int testId { get; set; }

    }
}

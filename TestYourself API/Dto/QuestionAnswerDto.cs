

using System.ComponentModel.DataAnnotations.Schema;
using TestYourself_API.Models;

namespace TestYourself_API.Dto
{
    public class QuestionAnswerDto
    {
        public int Id { get; set; }
        public string? Contain { get; set; } 
        public string? FirstAnswer { get; set; }
        public string? SecondAnswer { get; set; }
        public string? ChosenAnswer { get; set; }
        public string? CorrectAnswer { get; set; } 
        public int ResultScore { get; set; }
        public List<Question>? Questions { get; set; }
        public int CurrentIndex { get; set; } 
        public bool isCorrect { get; set; }
        public int testId { get; set; }
    }
}

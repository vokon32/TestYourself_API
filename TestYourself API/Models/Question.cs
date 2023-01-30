using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestYourself_API.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string? FirstAnswer { get; set; }
        public string? SecondAnswer { get; set; }
        public string? CorrectAnswer { get; set; }
        public string? ChosenAnswer { get; set; }
        public string? Contain { get; set; }
        public bool? isCorrect { get; set; }
        [ForeignKey("Test")]
        public int testId { get; set; }
        public Test? Test { get; set; }
        public TestResult? TestResult { get; set; }

    }
}

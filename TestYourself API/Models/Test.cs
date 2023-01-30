using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TestYourself_API.Data.Enum;

namespace TestYourself_API.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? questionsAmount { get; set; }
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double? Result { get; set; }
        public bool isPassed { get; set; }
        public bool CanBePassedAgain { get; set; } = false;
        public TestCategory TestCategory { get; set; }
        [ForeignKey("Questions")]
        public List<Question> Questions = new();
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public ICollection<TestResult>? TestResults { get; set; }
    }
}

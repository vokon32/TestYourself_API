

namespace TestYourself_API.Models
{
    public class TestResult
    {
        public int Id { get; set; }
        public int testId { get; set; }
        public Test? Test { get; set; }
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public double FinalScore { get; set; }
        public bool isPassed { get; set; }
    }
}

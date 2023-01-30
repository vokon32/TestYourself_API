
namespace TestYourself_API.Dto
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? questionsAmount { get; set; }
        public string Image { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool isPassed { get; set; } = false;
        public bool CanBePassedAgain { get; set; } = false;
    }
}

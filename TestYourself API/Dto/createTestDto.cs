using TestYourself_API.Data.Enum;

namespace TestYourself_API.Dto
{
    public class createTestDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public TestCategory? TestCategory { get; set; }
        public string AppUserId { get; set; } = null!;
    }
}

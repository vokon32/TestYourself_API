
using TestYourself_API.Data.Enum;

namespace TestYourself_API.Dto
{
    public class EditTestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public TestCategory TestCategory { get; set; }
    }
}

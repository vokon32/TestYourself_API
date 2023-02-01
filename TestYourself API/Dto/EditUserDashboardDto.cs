

namespace TestYourself_API.Dto
{
    public class EditUserDashboardDto
    {
        public string Id { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
    }
}

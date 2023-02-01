
using System.Threading.Tasks;

namespace TestYourself_API.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public int? TestPassed { get; set; }
        public string? ImageUrl { get; set; }

    }
}

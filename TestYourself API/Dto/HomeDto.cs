

using TestYourself_API.Models;

namespace TestYourself_API.Dto
{
    public class HomeDto
    {
        public IEnumerable<Test> Tests { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}

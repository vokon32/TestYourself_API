using Microsoft.AspNetCore.Identity;


namespace TestYourself_API.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public ICollection<Test>? Tests { get; set; }
        public List<TestResult>? TestResults { get; set; }
    }
}

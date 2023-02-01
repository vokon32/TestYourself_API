using System.ComponentModel.DataAnnotations;


namespace TestYourself_API.Dto
{
    public class LoginDto
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}


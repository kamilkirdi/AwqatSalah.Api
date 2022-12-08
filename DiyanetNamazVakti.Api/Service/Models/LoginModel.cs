namespace DiyanetNamazVakti.Api.Service.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [JsonPropertyName("email")]
        [Display(Name = nameof(Dictionary.Email), ResourceType = typeof(Dictionary))]
        public string Email { get; set; }

        [Required]
        [JsonPropertyName("password")]
        [Display(Name = nameof(Dictionary.Password), ResourceType = typeof(Dictionary))]
        public string Password { get; set; }


    }
}
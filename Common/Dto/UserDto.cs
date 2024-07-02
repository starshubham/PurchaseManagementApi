using System.ComponentModel.DataAnnotations;

namespace Common.Dto
{
    public class UserDto
    {
        // Person Table
        public int PersonId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateOnly DOB { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Salutation { get; set; }
        public string FullName { get; set; }
        public string MobNo { get; set; }
        public string Address { get; set; }

        // User table
        public int UserId { get; set; }
        [EmailAddress, Required]
        public string Email {  get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

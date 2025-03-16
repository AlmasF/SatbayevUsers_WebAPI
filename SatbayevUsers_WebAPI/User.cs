using SatbayevUsers_WebAPI.Validators;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SatbayevUsers_WebAPI
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"[a-zA-Z\s]*")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MinimumAge(18)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(12)]
        [RegularExpression(@"^\d{12}$")]
        public string IIN { get; set; } = null!;
    }
}

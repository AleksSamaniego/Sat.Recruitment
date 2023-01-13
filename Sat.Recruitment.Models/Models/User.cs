using Sat.Recruitment.Models.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Models.Models;

public class User
{
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required."), EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Phone is required."), Phone]
    public string? Phone { get; set; }

    public UserType? UserType { get; set; }

    [RegularExpression(@"^\d+(\.\d{1,2})?$")]
    [Range(0, 9999999999999999.99)]
    public decimal? Money { get; set; }
}

using Sat.Recruitment.Models.Enumerations;
using Sat.Recruitment.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Features;

public class CreateUserCommand : MediatR.IRequest<Result>
{
    [Required(ErrorMessage = "Name is required.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
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

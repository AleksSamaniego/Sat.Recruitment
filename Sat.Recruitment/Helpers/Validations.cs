using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Api.Features;

namespace Sat.Recruitment.Api.Helpers;

public class Validations
{
    public static string ValidateEmptyFields(CreateUserCommand user)
    {
        string errors = string.Empty;

        if (string.IsNullOrEmpty(user.Name))
            //Validate if Name is null
            errors = "The name is required";
        if (string.IsNullOrEmpty(user.Email))
            //Validate if Email is null
            errors = errors + " The email is required";
        if (string.IsNullOrEmpty(user.Address))
            //Validate if Address is null
            errors = errors + " The address is required";
        if (string.IsNullOrEmpty(user.Phone))
            //Validate if Phone is null
            errors = errors + " The phone is required";

        return errors;
    }

    public static User ValidateUserType(User user)
    {
        if (user.UserType == Models.Enumerations.UserType.Normal)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.12);
                //If new user is normal and has more than USD100
                var gif = user.Money * percentage;
                user.Money = user.Money + gif;
            }
            if (user.Money < 100)
            {
                if (user.Money > 10)
                {
                    var percentage = Convert.ToDecimal(0.8);
                    var gif = user.Money * percentage;
                    user.Money = user.Money + gif;
                }
            }
        }
        if (user.UserType == Models.Enumerations.UserType.SuperUser)
        {
            if (user.Money > 100)
            {
                var percentage = Convert.ToDecimal(0.20);
                var gif = user.Money * percentage;
                user.Money = user.Money + gif;
            }
        }
        if (user.UserType == Models.Enumerations.UserType.Premium)
        {
            if (user.Money > 100)
            {
                var gif = user.Money * 2;
                user.Money = user.Money + gif;
            }
        }

        return user;
    }
}

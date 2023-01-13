using MediatR;
using Sat.Recruitment.Models.Models;
using Sat.Recruitment.Api.Helpers;

namespace Sat.Recruitment.Api.Features.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private List<User> _users;
    public CreateUserCommandHandler(List<User> users)
    {
        _users = users;
    }

    public async Task<Result> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (command == null)
            return new Result() { IsSuccess = false, Errors = "No information was provided." };

        //Check for duplicates
        var duplicateFilter = _users.Where(x =>
        (x.Email == command.Email || x.Phone == command.Phone) || (x.Name == command.Name || x.Address == command.Address));

        if (duplicateFilter.Any())
            return new Result() { IsSuccess = false, Errors = "User is duplicated" };

        string validation = Validations.ValidateEmptyFields(command);

        if (!string.IsNullOrEmpty(validation))
            return new Result() { IsSuccess = false, Errors = validation };

        var user = new Models.Models.User()
        {
            Name = command.Name,
            Email = command.Email,
            Address = command.Address,
            Phone = command.Phone,
            UserType = command.UserType,
            Money = command.Money,
        };

        user = Validations.ValidateUserType(user);

        //Insert to List
        _users.Add(user);

        return new Result() { IsSuccess = true, Errors = "User Created" };
    }
}
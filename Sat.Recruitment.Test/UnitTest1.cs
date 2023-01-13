using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Features;
using Sat.Recruitment.Api.Features.Handlers;
using Sat.Recruitment.Models.Enumerations;
using Sat.Recruitment.Models.Models;

namespace Sat.Recruitment.Test
{
    public class UnitTest1
    {
        //this is used to test controller
        private readonly Mock<IMediator> _mockMediator;

        //Change this to your Users.txt path
        string path = @"C:\Users\alejandro_samaniego\Documents\Sat.Recruitment-main\Sat.Recruitment.Api\Files\Users.txt";

        public UnitTest1()
        {
            _mockMediator = new Mock<IMediator>(MockBehavior.Strict);
        }

        [Fact(DisplayName = "Create user Controller - Pass")]
        public void CreatUserControllerPass()
        {
            //Arrange
            var req = new CreateUserCommand()
            {
                Name = "Test",
                Email = "Test",
                Address = "",
                Phone = "Test",
                Money = 10,
                UserType = Models.Enumerations.UserType.Normal
            };

            var response = new Result()
            {
                IsSuccess = true,
                Errors = "User Created"
            };

            _mockMediator.Setup(m => m.Send(req, new CancellationToken()))
                .ReturnsAsync(response);

            UsersController controller = new UsersController(_mockMediator.Object);

            //Act
            var result = controller.CreateUser(req).Result as OkObjectResult;

            //Assert
            Assert.Equal(true, ((Result)result.Value).IsSuccess);
            Assert.Equal("User Created", ((Result)result.Value).Errors);
        }


        [Fact(DisplayName = "User is duplicated - Fail")]
        public async void UserDuplicatedFail()
        {
            //Arrange
            var req = new CreateUserCommand()
            {
                Name = "Test",
                Email = "Test",
                Address = "",
                Phone = "Test",
                Money = 10,
                UserType = UserType.Normal
            };

            var response = new Result()
            {
                IsSuccess = false,
                Errors = "User is duplicated"
            };

            _mockMediator.Setup(m => m.Send(req, new CancellationToken()))
                .ReturnsAsync(response);

            UsersController controller = new UsersController(_mockMediator.Object);

            //Act
            await controller.CreateUser(req);
            var result = controller.CreateUser(req).Result as OkObjectResult;

            //Assert
            Assert.Equal(false, ((Result)result.Value).IsSuccess);
            Assert.Equal("User is duplicated", ((Result)result.Value).Errors);
        }


        [Fact(DisplayName = "Multiple user creation - Fail")]
        public void MultipleUserCreationFail()
        {
            //Arrange
            var response = new Result()
            {
                IsSuccess = false,
                Errors = "User is duplicated"
            };

            string[] usersLines = File.ReadAllLines(path);

            List<User> usersList = new List<User>();

            foreach (string userLine in usersLines)
            {
                var line = userLine.Split(',');
                var user = new User()
                {
                    Name = line[0],
                    Email = line[1],
                    Phone = line[2],
                    Address = line[3],
                    UserType = ChangeUserType(line[4]),
                    Money = decimal.Parse(line[5]),
                };
                usersList.Add(user);
            }

            CreateUserCommandHandler handler = new CreateUserCommandHandler(usersList);

            var newUser = new CreateUserCommand()
            {
                Name = "Juan",
                Email = "Juan@marmol.com",
                Phone = "+5491154762312",
                Address = "Peru 2464",
                UserType = UserType.Normal,
                Money = 1234,
            };

            //Act
            var result = handler.Handle(newUser, new CancellationToken()).Result;

            //Assert
            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("User is duplicated", result.Errors);
        }

        // Single function to convert string to UserType enum
        private UserType ChangeUserType(string usertype)
        {
            if (usertype == "SuperUser")
                return UserType.SuperUser;

            if (usertype == "Premium")
                return UserType.Premium;

            return UserType.Normal;
        }
    }
}
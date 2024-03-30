using AutoFixture;
using Domain.Ports;
using Domain.Users;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class RegisterUserUseCaseTests
    {
        private readonly Fixture _fixture = new();
        Mock<IGoogleApiClient> _googleApiClientMock = new(MockBehavior.Strict);
        Mock<ICognitoClient> _cognitoClientMock = new(MockBehavior.Strict);
        User _user;
        string _bearerToken;

        public RegisterUserUseCaseTests()
        {
            _fixture.Register(() => new DateOnly());
            SetupForSuccess();
        }

        private void SetupForSuccess()
        {
            _user = _fixture.Create<User>();
            _bearerToken = _fixture.Create<string>();

            _googleApiClientMock.Setup(x=> x.GetUser(It.IsAny<string>()))
                .ReturnsAsync(_user);
            _cognitoClientMock
                .Setup(x => x.RegisterUser(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public void ExecuteAsync_CallsGetUser()
        {
            string accessCode = _fixture.Create<string>();

            var sut = CreateSut();

            sut.ExecuteAsync(_bearerToken);

            _googleApiClientMock.Verify(x=> x.GetUser(_bearerToken), Times.Once());
        }

        [Fact]
        public async void ExecuteAsync_WhenGoogleClientThrows_Throws()
        {
            string accessCode = _fixture.Create<string>();
            string exceptionMessage = _fixture.Create<string>();

            _googleApiClientMock.Setup(x => x.GetUser(It.IsAny<string>()))
                .Throws(new InvalidOperationException(exceptionMessage));

            var sut = CreateSut();

            await sut.Awaiting(x=> x.ExecuteAsync(_bearerToken))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void ExecuteAsync_CallsCognitoToRegisterUser()
        {
            string accessCode = _fixture.Create<string>();

            var sut = CreateSut();

            sut.ExecuteAsync(_bearerToken);

            _cognitoClientMock.Verify(x => x.RegisterUser(_user.emailAddress, _user.Id), Times.Once());
        }

        private IRegisterUserUseCase CreateSut() =>
            new RegisterUserUseCase(_googleApiClientMock.Object, _cognitoClientMock.Object);
    }
}
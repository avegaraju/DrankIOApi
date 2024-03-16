using AutoFixture;
using Domain.Ports;
using Domain.Users;
using DrankIO.Domain.Ports;
using DrankIO.Domain.Users;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class RegisterUserUseCaseTests
    {
        private readonly Fixture _fixture = new();
        Mock<ICognitoClient> _cognitoClientMock = new(MockBehavior.Strict);
        Mock<IGoogleApiClient> _googleApiClientMock = new(MockBehavior.Strict);
        User _user;
        string _token;

        public RegisterUserUseCaseTests()
        {
            SetupForSuccess();
        }

        private void SetupForSuccess()
        {
            _user = _fixture.Create<User>();
            _token = _fixture.Create<string>();

            _googleApiClientMock.Setup(x=> x.GetUser(It.IsAny<string>()))
                .ReturnsAsync(_user);
            _cognitoClientMock.Setup(x => x.GetToken(It.IsAny<string>()))
                .ReturnsAsync(_token);
        }

        [Fact]
        public void ExecuteAsync_GetsUserInformation()
        {
            string email = _fixture.Create<string>();
            string accessCode = _fixture.Create<string>();

            var sut = CreateSut();

            sut.ExecuteAsync(email);

            _googleApiClientMock.Verify(x=> x.GetUser(email), Times.Once());
        }

        [Fact]
        public async void ExecuteAsync_WhenGoogleClientThrows_Throws()
        {
            string email = _fixture.Create<string>();
            string accessCode = _fixture.Create<string>();
            string exceptionMessage = _fixture.Create<string>();

            _googleApiClientMock.Setup(x => x.GetUser(It.IsAny<string>()))
                .Throws(new InvalidOperationException(exceptionMessage));

            var sut = CreateSut();

            await sut.Awaiting(x=> x.ExecuteAsync(email))
                .Should().ThrowAsync<Exception>();
        }

        private IRegisterUserUseCase CreateSut() =>
            new RegisterUserUseCase(_cognitoClientMock.Object, _googleApiClientMock.Object);
    }
}
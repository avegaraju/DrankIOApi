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
        string _accessCode;

        public RegisterUserUseCaseTests()
        {
            _fixture.Register(() => new DateOnly());
            SetupForSuccess();
        }

        private void SetupForSuccess()
        {
            _user = _fixture.Create<User>();
            _token = _fixture.Create<string>();
            _accessCode = _fixture.Create<string>();

            _googleApiClientMock.Setup(x=> x.GetUser(It.IsAny<string>()))
                .ReturnsAsync(_user);
            _cognitoClientMock.Setup(x => x.GetToken(It.IsAny<string>()))
                .ReturnsAsync(_token);
        }

        [Fact]
        public void ExecuteAsync_CallsGetUser()
        {
            string accessCode = _fixture.Create<string>();

            var sut = CreateSut();

            sut.ExecuteAsync(_accessCode);

            _googleApiClientMock.Verify(x=> x.GetUser(_token), Times.Once());
        }

        [Fact]
        public async void ExecuteAsync_WhenGoogleClientThrows_Throws()
        {
            string accessCode = _fixture.Create<string>();
            string exceptionMessage = _fixture.Create<string>();

            _googleApiClientMock.Setup(x => x.GetUser(It.IsAny<string>()))
                .Throws(new InvalidOperationException(exceptionMessage));

            var sut = CreateSut();

            await sut.Awaiting(x=> x.ExecuteAsync(_accessCode))
                .Should().ThrowAsync<Exception>();
        }

        [Fact]
        public void ExecuteAsync_CallsCognitoClientForBearerToken()
        {
            var sut = CreateSut();

            sut.ExecuteAsync(_accessCode);

            _cognitoClientMock.Verify(x => x.GetToken(_accessCode), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_WhenCognitoClientThrows_Throws()
        {
            _cognitoClientMock.Setup(
                x => x.GetToken(It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException()
                );

            var sut = CreateSut();

            await sut.Awaiting(
                x=>x.ExecuteAsync(_accessCode)
                )
                .Should()
                .ThrowAsync<InvalidOperationException>();
        }

        private IRegisterUserUseCase CreateSut() =>
            new RegisterUserUseCase(_cognitoClientMock.Object, _googleApiClientMock.Object);
    }
}
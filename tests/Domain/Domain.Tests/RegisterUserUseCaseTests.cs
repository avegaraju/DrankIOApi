using AutoFixture;
using Domain.Ports;
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

        public RegisterUserUseCaseTests()
        {
            SetupForSuccess();
        }

        private void SetupForSuccess()
        {
            _googleApiClientMock.Setup(x=> x.Validate(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            _cognitoClientMock.Setup(x => x.RegisterUser(It.IsAny<string>()))
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public void ExecuteAsync_InvokesGoogleClient()
        {
            string email = _fixture.Create<string>();
            string accessCode = _fixture.Create<string>();

            var sut = CreateSut();

            sut.ExecuteAsync(email, accessCode);

            _googleApiClientMock.Verify(x=> x.Validate(accessCode), Times.Once());
        }

        [Fact]
        public async void ExecuteAsync_WhenGoogleClientThrows_Throws()
        {
            string email = _fixture.Create<string>();
            string accessCode = _fixture.Create<string>();
            string exceptionMessage = _fixture.Create<string>();

            _googleApiClientMock.Setup(x => x.Validate(It.IsAny<string>()))
                .Throws(new InvalidOperationException(exceptionMessage));

            var sut = CreateSut();

            await sut.Awaiting(x=> x.ExecuteAsync(email, accessCode))
                .Should().ThrowAsync<Exception>();
        }

        private IRegisterUserUseCase CreateSut() =>
            new RegisterUserUseCase(_cognitoClientMock.Object, _googleApiClientMock.Object);
    }
}
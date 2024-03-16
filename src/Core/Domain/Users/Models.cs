using System;

namespace Domain.Users
{
    public record class User(
        string Id,
        string FirstName,
        string LastName,
        string ProfilePictureUrl,
        string Gender,
        DateOnly DateOfBirth,
        string emailAddress
        );
}

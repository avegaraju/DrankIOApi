using Domain.Users;

namespace Google
{
    internal record class UserProfileDto(
        Name[] Names,
        Photo[] Photos,
        Gender[] Genders,
        Birthday[] Birthdays,
        EmailAddress[] EmailAddresses
        )
    {
        internal User ToDomain()
        {
            var name = Names.ToList().Single(x => x.Metadata.Primary);
            var birthDate = Birthdays.ToList().Single(x=>x.Metadata.Source.Type.ToLower() == "account").Date;
            var dateOfBirth = new DateOnly((int)birthDate.Year, (int)birthDate.Month, (int)birthDate.Day);

            return new User(
                Id: name.Metadata.Source.Id,
                FirstName: name.GivenName,
                LastName: name.FamilyName,
                ProfilePictureUrl: Photos.ToList().Single().Url,
                Gender: Genders.ToList().Single().FormattedValue,
                DateOfBirth: dateOfBirth,
                emailAddress: EmailAddresses.Single(x => x.Metadata.Primary == true).Value
                );
        }
    }

    internal record class Name(
        Metadata Metadata,
        string GivenName,
        string FamilyName
        );

    internal record class Metadata(
        bool Primary,
        bool Verified,
        Source Source
        );

    internal record class Source(string Type, string Id);

    internal record class Photo(string Url);

    internal record class Gender(string FormattedValue);

    internal record class Birthday(Metadata Metadata, Date Date);

    internal record class Date(uint Year, uint Month, uint Day);

    internal record class EmailAddress(Metadata Metadata, string Value);
}

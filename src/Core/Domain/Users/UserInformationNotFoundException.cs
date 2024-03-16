using System;

namespace Domain.Users
{
    public class UserInformationNotFoundException: Exception
    {
        public UserInformationNotFoundException(string message)
            :base($"User information not accessible. {message}")
        {
            
        }
    }
}

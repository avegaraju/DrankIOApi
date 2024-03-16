using System;

namespace Domain.Users
{
    public class UserInformationNotFoundException: Exception
    {
        public UserInformationNotFoundException()
            :base("User information not accessible.")
        {
            
        }
    }
}

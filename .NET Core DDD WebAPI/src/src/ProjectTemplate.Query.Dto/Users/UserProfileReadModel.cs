using System;

namespace ProjectTemplate.Query.Dto.Users
{
    public class UserProfileReadModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

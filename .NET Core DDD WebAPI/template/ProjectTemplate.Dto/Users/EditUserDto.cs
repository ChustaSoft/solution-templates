using System;

namespace $ext_safeprojectname$.Dto.Users
{
    public class EditUserDto
    {
        public Guid UserId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string RecoveryEmail { get; init; }
    }
}

namespace $ext_safeprojectname$.Dto.Users
{
    public class RegisterUserDto
    {
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string RecoveryEmail { get; init; }
    }
}

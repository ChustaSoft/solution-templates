using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Domain.Aggregates.Users
{
    public class FirstName : ValueObject<FirstName>
    {
        private readonly string firstName;

        public FirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new InvalidOperationException("First name cannot be empty");

            if (firstName.Length is < 1 or > 50)
                throw new InvalidOperationException("Length of first name must be between 1 and 50 characters");

            this.firstName = firstName; 
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return firstName;
        }

        public override string ToString()
        {
            return firstName;
        }

        public static implicit operator string(FirstName _firstName) => _firstName.ToString();

        public static implicit operator FirstName(string _firstName) => new FirstName(_firstName);
    }
}

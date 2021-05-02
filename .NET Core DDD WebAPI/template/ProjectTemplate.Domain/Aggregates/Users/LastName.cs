using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Domain.Aggregates.Users
{
    public class LastName : ValueObject<LastName>
    {
        private readonly string lastName;

        public LastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new InvalidOperationException("Last name cannot be empty");

            if (lastName.Length is < 1 or > 100)
                throw new InvalidOperationException("Length of last name must be between 1 and 50 characters");

            this.lastName = lastName; 
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return lastName;
        }

        public override string ToString()
        {
            return lastName;
        }

        public static implicit operator string(LastName _lastName) => _lastName.ToString();

        public static implicit operator LastName(string _lastName) => new LastName(_lastName);
    }
}

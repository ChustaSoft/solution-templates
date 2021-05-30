using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace $ext_safeprojectname$.Domain.Shared.ValueTypes
{
    public class Email : ValueObject<Email>
    {
        private string EmailAddress { get; }

        public Email(string emailAddress)
        {
            try
            {
                new MailAddress(emailAddress);
                EmailAddress = emailAddress;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Email is not valid", exception);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }

        public override string ToString()
        {
            return EmailAddress;
        }

        public static implicit operator string(Email email) => email.ToString();

        public static implicit operator Email(string email) => new Email(email);
    }
}

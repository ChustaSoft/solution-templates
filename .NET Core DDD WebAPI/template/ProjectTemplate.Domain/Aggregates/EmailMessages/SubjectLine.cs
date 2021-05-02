using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Domain.Aggregates.EmailMessages
{
    public class SubjectLine : ValueObject<SubjectLine>
    {
        private const int MAX_NUMBER_OR_CHARACTERS = 10000;
        private readonly string subjectLine;

        public SubjectLine(string subjectLine)
        {
            if (string.IsNullOrWhiteSpace(subjectLine))
            {
                throw new InvalidOperationException("Subjectline cannot be empty");
            }
            else if (subjectLine.Length > MAX_NUMBER_OR_CHARACTERS)
            {
                throw new InvalidOperationException($"Subjectline cannot contain more than {MAX_NUMBER_OR_CHARACTERS} characters");
            }
            else
            {
                this.subjectLine = subjectLine;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return subjectLine;
        }

        public override string ToString()
        {
            return subjectLine;
        }

        public static implicit operator string(SubjectLine subjectLine) => subjectLine.ToString();

        public static implicit operator SubjectLine(string subjectLine) => new SubjectLine(subjectLine);
    }
}

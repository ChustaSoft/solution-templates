using $ext_safeprojectname$.Framework.Aggregates;
using System;
using System.Collections.Generic;

namespace $ext_safeprojectname$.Domain.Aggregates.EmailMessages
{
    public class TextContent : ValueObject<TextContent>
    {
        private const int MAX_NUMBER_OR_CHARACTERS = 1000000;
        private readonly string textContent;

        public TextContent(string textContent)
        {
            if (string.IsNullOrWhiteSpace(textContent))
            {
                throw new InvalidOperationException("Content of the email cannot be empty");
            }
            else if (textContent.Length > MAX_NUMBER_OR_CHARACTERS)
            {
                throw new InvalidOperationException($"Content of email cannot be more than {MAX_NUMBER_OR_CHARACTERS} characters");
            }
            else
            {
                this.textContent = textContent;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return textContent;
        }

        public override string ToString()
        {
            return textContent;
        }

        public static implicit operator string(TextContent textContent) => textContent.ToString();

        public static implicit operator TextContent(string textContent) => new TextContent(textContent);
    }
}

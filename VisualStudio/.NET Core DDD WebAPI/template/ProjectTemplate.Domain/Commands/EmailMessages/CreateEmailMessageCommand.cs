using $ext_safeprojectname$.Domain.Shared.ValueTypes;
using $ext_safeprojectname$.Framework.Commands;

namespace $ext_safeprojectname$.Domain.Commands.EmailMessages
{
    public class CreateEmailMessageCommand : CreationCommand
    {
        public CreateEmailMessageCommand(string sender, string subjectLine, string textContent, string destination)
        {
            if (string.IsNullOrWhiteSpace(subjectLine) || subjectLine.Length > 1000)
                throw new System.ArgumentException($"'{nameof(subjectLine)}' contains invalid value", nameof(subjectLine));

            Sender = new Email(sender);
            SubjectLine = subjectLine;
            TextContent = textContent;
            Destination = destination;
        }

        public Email Sender { get; init; }
        public string SubjectLine { get; init; }
        public string TextContent { get; }
        public Email Destination { get; }
    }
}

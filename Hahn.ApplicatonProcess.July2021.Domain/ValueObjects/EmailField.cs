using Hahn.ApplicationProcess.July2021.Domain.Exceptions;

namespace Hahn.ApplicationProcess.July2021.Domain.ValueObjects
{
    public class EmailField : BaseValueObject<EmailField>
    {
        public EmailField(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please enter the email address");
            }

            if (IsValidEmail(value) == false)
            {
                throw new InvalidValueObjectStateException("Email address is not valid");
            }

            Value = value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(EmailField otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static EmailField CreateIfNotEmpty(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return null;
            }

            return new EmailField(mobile);
        }
    }
}
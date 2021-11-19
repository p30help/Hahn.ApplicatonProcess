using Hahn.ApplicationProcess.July2021.Domain.Exceptions;

namespace Hahn.ApplicationProcess.July2021.Domain.ValueObjects
{
    public class HumanName : BaseValueObject<HumanName>
    {
        public HumanName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidValueObjectStateException("Please enter the name");
            }

            if (value.Length < 3)
            {
                throw new InvalidValueObjectStateException("name must be at least 3 characters");
            }

            Value = value;
        }

        public string Value { get; }

        public override bool ObjectIsEqual(HumanName otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }

        public static HumanName CreateIfNotEmpty(string mobile)
        {
            if (string.IsNullOrWhiteSpace(mobile))
            {
                return null;
            }

            return new HumanName(mobile);
        }
    }
}
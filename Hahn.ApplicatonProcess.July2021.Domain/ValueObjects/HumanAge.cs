using Hahn.ApplicationProcess.July2021.Domain.Exceptions;

namespace Hahn.ApplicationProcess.July2021.Domain.ValueObjects
{
    public class HumanAge : BaseValueObject<HumanAge>
    {

        public HumanAge(int value)
        {
            if (value < 18 || value > 120)
            {
                throw new InvalidValueObjectStateException("Age must be more than 18 and less than 120");
            }

            this.Value = value;
        }

        public int Value { get; }

        public override bool ObjectIsEqual(HumanAge otherObject)
        {
            return Value == otherObject.Value;
        }

        public override int ObjectGetHashCode()
        {
            return GetHashCode();
        }
    }
}
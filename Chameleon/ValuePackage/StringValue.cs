using System;

namespace Chameleon.ValuePackage
{
    public class StringValue : Value
    {
        private string value;

        public StringValue(string value)
        {
            this.value = value;
        }

        public Value Evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return value;
        }

        public double ToNumber()
        {
            return double.Parse(value);
        }
    }
}

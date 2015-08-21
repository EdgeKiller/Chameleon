namespace Chameleon.ValuePackage
{
    public class NumberValue : Value
    {
        private double value;

        public NumberValue(double value)
        {
            this.value = value;
        }

        public Value Evaluate()
        {
            return this;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public double ToNumber()
        {
            return value;
        }
    }
}

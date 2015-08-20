using Chameleon.ExpressionPackage;

namespace Chameleon.ValuePackage
{
    public interface Value : Expression
    {
        string ToString();

        double ToNumber();
    }
}

namespace Chameleon.StatementPackage
{
    public class GotoStatement : Statement
    {
        private string label;
        private Chameleon chameleon;

        public GotoStatement(string label, Chameleon chameleon)
        {
            this.label = label;
            this.chameleon = chameleon;
        }

        public void Execute()
        {
            if (chameleon.labels.ContainsKey(label))
            {
                chameleon.currentStatement = chameleon.labels[label];
            }
        }
    }
}

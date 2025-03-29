using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Decorators
{
    public class BTConditional : INode
    {
        private Func<BTContext, bool> condition;
        private INode node;

        public BTConditional(Func<BTContext, bool> condition, INode node)
        {
            this.condition = condition;
            this.node = node;
        }

        public BTResult Run(BTContext btContext)
        {
            if (condition(btContext) == false)
            {
                return BTResult.Failure;
            }

            return node.Run(btContext);
        }

        public void Reset()
        {
            node.Reset();
        }
    }
}
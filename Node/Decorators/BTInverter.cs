using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Decorators
{
    public class BTInverter : INode
    {
        private INode node;

        public BTInverter(INode node)
        {
            this.node = node;
        }

        public BTResult Run(BTContext btContext)
        {
            var result = node.Run(btContext);

            if (result == BTResult.Success)
            {
                return BTResult.Failure;
            }
            else if (result == BTResult.Failure)
            {
                return BTResult.Success;
            }

            return result;
        }
        public void Reset()
        {
            node.Reset();
        }
    }
}

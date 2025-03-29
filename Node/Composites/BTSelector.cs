using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Composites
{
    public class BTSelector : INode
    {
        private INode[] nodes;

        public BTSelector(params INode[] nodes)
        {
            this.nodes = nodes;
        }

        public BTResult Run(BTContext btContext)
        {
            foreach (var node in nodes)
            {
                var result = node.Run(btContext);
                if (result != BTResult.Failure)
                {
                    return result;
                }
            }

            return BTResult.Failure;
        }

        public void Reset()
        {
            foreach (var node in nodes)
            {
                node.Reset();
            }
        }
    }
}

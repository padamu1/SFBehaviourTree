using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Composites
{
    public class BTSequence : INode
    {
        private int index;
        private List<INode> nodes;

        public BTSequence()
        {
            index = 0;
            nodes = new List<INode>();
        }

        public BTSequence Node(INode node)
        {
            nodes.Add(node);
            return this;
        }

        public BTResult Run(BTContext btContext)
        {
            BTResult nodeResult = nodes[index].Run(btContext);
            if (nodeResult == BTResult.Success)
            {
                ++index;

                if (index >= nodes.Count)
                {
                    index = 0;
                }
            }
            else if (nodeResult == BTResult.Failure)
            {
                index = 0;
            }

            return nodeResult;
        }

        public void Reset()
        {
            index = 0;
            foreach (INode node in nodes)
            {
                node.Reset();
            }
        }
    }
}

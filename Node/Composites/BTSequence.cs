using System.Collections.Generic;
using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Composites
{
    public class BTSequence : INode
    {
        private int index;

        private List<INode> nodes;

        public BTSequence()
        {
            nodes = new List<INode>();
        }

        public BTSequence Node(INode node)
        {
            nodes.Add(node);
            return this;
        }

        public BTResult Run(BTContext btContext)
        {
            while (index < nodes.Count)
            {
                BTResult nodeResult = nodes[index].Run(btContext);
                if (nodeResult == BTResult.Failure)
                {
                    return BTResult.Failure;
                }
                else if (nodeResult == BTResult.Running)
                {
                    return BTResult.Running;
                }
                 
                index++;
            }

            return BTResult.Success;
        }
    }
}

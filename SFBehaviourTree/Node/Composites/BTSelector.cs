using System.Collections.Generic;
using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Composites
{
    public class BTSelector : INode
    {
        private List<INode> nodes;
        private int index;

        public BTSelector()
        {
            this.nodes = new List<INode>();
        }

        public void AddNode(INode node)
        {
            nodes.Add(node);
        }

        public BTResult Run(BTContext btContext)
        {
            while (index < nodes.Count)
            {
                var result = nodes[index].Run(btContext);
                if (result == BTResult.Success)
                {
                    index = 0;
                    return BTResult.Success;
                }
                else if (result == BTResult.Running)
                {
                    return BTResult.Running;
                }

                index++;
            }

            index = 0;
            return BTResult.Failure;
        }
    }
}

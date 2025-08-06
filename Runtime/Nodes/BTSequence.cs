using System.Collections.Generic;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTSequence : IBTNode
    {
        readonly string nodeName;

        readonly List<IBTNode> childrens;

        public BTSequence(string nodeName)
        {
            this.nodeName = nodeName;
            childrens = new List<IBTNode>();
        }

        public BTSequence(string nodeName, List<IBTNode> children)
        {
            this.nodeName = nodeName;
            this.childrens = children;
        }

        public void AddChild(IBTNode child)
        {
            childrens.Add(child);
        }

        public NodeState Run(BTContext btContext)
        {
            // Run each behaviour tree child node in sequence
            foreach (var child in childrens)
            {
                var state = child.Run(btContext);
                if (state != NodeState.Success)
                {
                    // If any child fails, the sequence fails
                    return state;
                }
            }

            // If all children succeed, the sequence succeeds
            return NodeState.Success;
        }
    }
}

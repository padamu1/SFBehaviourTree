using System.Collections.Generic;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTSelector : IBTNode
    {
        readonly string nodeName;

        readonly List<IBTNode> childrens;

        public BTSelector(string nodeName)
        {
            this.nodeName = nodeName;
            this.childrens = new List<IBTNode>();
        }

        public BTSelector(string nodeName, List<IBTNode> childrens)
        {
            this.nodeName = nodeName;
            this.childrens = childrens;
        }

        public void AddChild(IBTNode child)
        {
            childrens.Add(child);
        }

        public NodeState Run(BTContext btContext)
        {
            // Run each behaviour tree child node in selector mode
            foreach (var child in childrens)
            {
                var state = child.Run(btContext);
                if (state != NodeState.Failure)
                {
                    // If any child succeeds or is running, the selector succeeds
                    return state;
                }
            }

            // If all children fail, the selector fails
            return NodeState.Failure;
        }
    }
}

using System;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTAction : IBTNode
    {
        readonly string nodeName;

        readonly Func<BTContext, NodeState> action;

        public BTAction(string nodeName, Func<BTContext, NodeState> action)
        {
            this.nodeName = nodeName;
            this.action = action;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTAction does not support adding children directly. Use the action property instead.");
        }

        public NodeState Run(BTContext btContext)
        {
            return action(btContext);
        }
    }
}

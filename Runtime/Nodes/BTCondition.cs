using System;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTCondition : IBTNode
    {
        readonly string nodeName;
        readonly Func<BTContext, bool> condition;

        public BTCondition(string nodeName, Func<BTContext, bool> condition)
        {
            this.nodeName = nodeName;
            this.condition = condition;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTCondition does not support adding children.");
        }

        public NodeState Run(BTContext btContext)
        {
            try
            {
                bool result = condition(btContext);
                return result ? NodeState.Success : NodeState.Failure;
            }
            catch (Exception)
            {
                return NodeState.Failure;
            }
        }
    }
} 
using System;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTInverter : IBTNode
    {
        readonly string nodeName;
        readonly IBTNode child;

        public BTInverter(string nodeName, IBTNode child)
        {
            this.nodeName = nodeName;
            this.child = child;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTInverter only supports one child. Set it in the constructor.");
        }

        public NodeState Run(BTContext btContext)
        {
            if (child == null)
            {
                return NodeState.Failure;
            }

            var childState = child.Run(btContext);
            
            switch (childState)
            {
                case NodeState.Success:
                    return NodeState.Failure;
                case NodeState.Failure:
                    return NodeState.Success;
                default:
                    return childState; // Running, Exit 등은 그대로 반환
            }
        }
    }
} 
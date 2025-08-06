using System;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTDecorator : IBTNode
    {
        readonly string nodeName;
        readonly IBTNode child;
        readonly Func<BTContext, NodeState, NodeState> decoratorFunction;

        public BTDecorator(string nodeName, IBTNode child, Func<BTContext, NodeState, NodeState> decoratorFunction)
        {
            this.nodeName = nodeName;
            this.child = child;
            this.decoratorFunction = decoratorFunction;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTDecorator only supports one child. Set it in the constructor.");
        }

        public NodeState Run(BTContext btContext)
        {
            if (child == null)
            {
                return NodeState.Failure;
            }

            var childState = child.Run(btContext);
            return decoratorFunction(btContext, childState);
        }
    }
} 
using System;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTRepeater : IBTNode
    {
        readonly string nodeName;
        readonly IBTNode child;
        readonly int repeatCount;
        readonly bool infinite;
        private int currentCount;

        public BTRepeater(string nodeName, IBTNode child, int repeatCount = -1)
        {
            this.nodeName = nodeName;
            this.child = child;
            this.repeatCount = repeatCount;
            this.infinite = repeatCount < 0;
            this.currentCount = 0;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTRepeater only supports one child. Set it in the constructor.");
        }

        public NodeState Run(BTContext btContext)
        {
            if (child == null)
            {
                return NodeState.Failure;
            }

            if (infinite || currentCount < repeatCount)
            {
                var childState = child.Run(btContext);
                
                if (childState == NodeState.Success)
                {
                    currentCount++;
                    if (infinite || currentCount < repeatCount)
                    {
                        return NodeState.Running; // 계속 반복
                    }
                    else
                    {
                        currentCount = 0; // 리셋
                        return NodeState.Success;
                    }
                }
                else if (childState == NodeState.Failure)
                {
                    currentCount = 0; // 리셋
                    return NodeState.Failure;
                }
                
                return childState; // Running, Exit 등은 그대로 반환
            }
            
            currentCount = 0; // 리셋
            return NodeState.Success;
        }
    }
} 
using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Decorators
{
    public class BTRepeater : INode
    {
        private INode node;
        private int repeatCount;

        private int playCount;

        public BTRepeater(int repeatCount, INode node)
        {
            this.repeatCount = repeatCount;
            this.node = node;
        }

        public BTResult Run(BTContext btContext)
        {
            var result = node.Run(btContext);
            if (result == BTResult.Success)
            {
                ++playCount;
            }
            else if (result == BTResult.Failure)
            {
                return BTResult.Failure;
            }

            return playCount >= repeatCount ? BTResult.Success : BTResult.Running;
        }

        public void Reset()
        {
            playCount = 0;
            node.Reset();
        }
    }
}

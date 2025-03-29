using SFBehaviourTree.Context;
using SFBehaviourTree.Random;

namespace SFBehaviourTree.Node.Composites
{
    public class BTRandomSelector : INode
    {
        private INode[] nodes;

        private BTResult prevResult = BTResult.Success;

        private int selectIndex;

        public BTRandomSelector(params INode[] nodes)
        {
            this.nodes = nodes;
        }

        public BTResult Run(BTContext btContext)
        {
            if (prevResult != BTResult.Running)
            {
                selectIndex = RInt.GetRandom(nodes.Length);
            }

            prevResult = nodes[selectIndex].Run(btContext);
            return prevResult;
        }

        public void Reset()
        {
            prevResult = BTResult.Success;
            selectIndex = 0;
            foreach (var node in nodes)
            {
                node.Reset();
            }
        }
    }
}

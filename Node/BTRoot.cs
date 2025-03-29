using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node
{
    public class BTRoot
    {
        private INode node;
        private BTContext btContext;

        public BTRoot(INode node)
        {
            this.node = node;
        }

        public void SetContext(BTContext btContext)
        {
            this.btContext = btContext;
        }

        public BTContext GetContext()
        {
            return btContext;
        }

        public void Reset()
        {
            btContext.Clear();
        }

        public bool Update()
        {
            return node.Run(btContext) != BTResult.Failure;
        }
    }
}
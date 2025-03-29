using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node
{
    public class BTAction : INode
    {
        private Func<BTContext, BTResult> action;

        public BTAction(Func<BTContext, BTResult> action)
        {
            this.action = action;
        }

        public BTResult Run(BTContext btContext)
        {
            return action(btContext);
        }

        public void Reset()
        {

        }
    }
}
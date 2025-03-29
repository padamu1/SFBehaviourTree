using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node
{
    public interface INode
    {
        public BTResult Run(BTContext btContext);
        public void Reset();
    }
}

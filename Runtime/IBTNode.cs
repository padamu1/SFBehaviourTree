using UnityEngine;

namespace SimulFactory.BehaviourTree
{
    public interface IBTNode
    {
        void AddChild(IBTNode child);
        NodeState Run(BTContext btContext);
    }
}

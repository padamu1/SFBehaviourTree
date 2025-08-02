using System;
using SFBehaviourTree.Context;

namespace SFBehaviourTree.Node.Decorators
{
    public class BTConditional : INode
    {
        private Func<BTContext, bool> condition;

        public BTConditional(Func<BTContext, bool> condition)
        {
            this.condition = condition;
        }

        public BTResult Run(BTContext btContext)
        {
            return condition(btContext) ? BTResult.Success : BTResult.Failure;
        }
    }
}
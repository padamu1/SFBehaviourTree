using SFBehaviourTree.Context;
using SFBehaviourTree.Node;
using SFBehaviourTree.Node.Composites;
using SFBehaviourTree.Node.Decorators;

namespace SFBehaviourTree.Core
{
    public class BTBuilder
    {
        private Stack<BTSequence> sequenceStack = new();
        private BTRoot root;
        private BTContext btContext;

        public static BTBuilder Root() => new BTBuilder();

        public BTBuilder SetContext(BTContext btContext)
        {
            this.btContext = btContext;
            return this;
        }

        public BTBuilder Sequence()
        {
            var seq = new BTSequence();
            sequenceStack.Push(seq);
            return this;
        }

        public BTBuilder Selector(params INode[] nodes)
        {
            sequenceStack.Peek().Node(new BTSelector(nodes));
            return this;
        }

        public BTBuilder RandomSelector(params INode[] nodes)
        {
            sequenceStack.Peek().Node(new BTRandomSelector(nodes));
            return this;
        }

        public BTBuilder Conditional(Func<BTContext, bool> condition, INode node)
        {
            sequenceStack.Peek().Node(new BTConditional(condition, node));
            return this;
        }

        public BTBuilder Action(Func<BTContext, BTResult> action)
        {
            sequenceStack.Peek().Node(new BTAction(action));
            return this;
        }

        public BTBuilder Inverter(INode node)
        {
            sequenceStack.Peek().Node(new BTInverter(node));
            return this;
        }

        public BTBuilder Repeater(int repeatCount, INode node)
        {
            sequenceStack.Peek().Node(new BTRepeater(repeatCount, node));
            return this;
        }

        public BTBuilder End()
        {
            var finished = sequenceStack.Pop();
            if (sequenceStack.Count > 0)
                sequenceStack.Peek().Node(finished);
            else
                root = new BTRoot(finished);

            root.SetContext(btContext);
            return this;
        }

        public BTRoot Build() => root;
    }
}
using SimulFactory.BehaviourTree.Nodes;
using System;
using System.Collections.Generic;

namespace SimulFactory.BehaviourTree.Helper
{
    /// <summary>
    /// Make Behaviour tree more readable and easier to build.
    /// </summary>
    public class BTBuilder
    {
        private IBTNode currentNode;
        private Stack<IBTNode> nodeStack = new Stack<IBTNode>();
        private bool isBuilding = false;

        /// <summary>
        /// Start building a new behaviour tree
        /// </summary>
        /// <returns>BTBuilder instance for fluent API</returns>
        public static BTBuilder Build()
        {
            return new BTBuilder();
        }

        /// <summary>
        /// Create a sequence node
        /// </summary>
        /// <param name="nodeName">Name of the sequence node</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Sequence(string nodeName)
        {
            var sequence = new BTSequence(nodeName);
            
            if (currentNode != null)
            {
                currentNode.AddChild(sequence);
                nodeStack.Push(currentNode);
            }
            
            currentNode = sequence;
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create a selector node
        /// </summary>
        /// <param name="nodeName">Name of the selector node</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Selector(string nodeName)
        {
            var selector = new BTSelector(nodeName);
            
            if (currentNode != null)
            {
                currentNode.AddChild(selector);
                nodeStack.Push(currentNode);
            }
            
            currentNode = selector;
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create an action node
        /// </summary>
        /// <param name="nodeName">Name of the action node</param>
        /// <param name="action">Action to execute</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Do(string nodeName, Func<BTContext, NodeState> action)
        {
            var actionNode = new BTAction(nodeName, action);
            
            if (currentNode != null)
            {
                currentNode.AddChild(actionNode);
            }
            else
            {
                currentNode = actionNode;
            }
            
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create a condition node
        /// </summary>
        /// <param name="nodeName">Name of the condition node</param>
        /// <param name="condition">Condition to check</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Condition(string nodeName, Func<BTContext, bool> condition)
        {
            var conditionNode = new BTCondition(nodeName, condition);
            
            if (currentNode != null)
            {
                currentNode.AddChild(conditionNode);
            }
            else
            {
                currentNode = conditionNode;
            }
            
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create a wait node
        /// </summary>
        /// <param name="nodeName">Name of the wait node</param>
        /// <param name="waitTime">Time to wait in seconds</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Wait(string nodeName, float waitTime)
        {
            var waitNode = new BTWait(nodeName, waitTime);
            
            if (currentNode != null)
            {
                currentNode.AddChild(waitNode);
            }
            else
            {
                currentNode = waitNode;
            }
            
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create an inverter node (inverts the result of the next node)
        /// </summary>
        /// <param name="nodeName">Name of the inverter node</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Inverter(string nodeName)
        {
            // Inverter는 다음 노드를 자식으로 가져야 하므로 스택에 저장
            var inverter = new BTInverter(nodeName, null);
            
            if (currentNode != null)
            {
                currentNode.AddChild(inverter);
                nodeStack.Push(currentNode);
            }
            
            currentNode = inverter;
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// Create a repeater node
        /// </summary>
        /// <param name="nodeName">Name of the repeater node</param>
        /// <param name="repeatCount">Number of times to repeat (-1 for infinite)</param>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder Repeater(string nodeName, int repeatCount = -1)
        {
            // Repeater는 다음 노드를 자식으로 가져야 하므로 스택에 저장
            var repeater = new BTRepeater(nodeName, null, repeatCount);
            
            if (currentNode != null)
            {
                currentNode.AddChild(repeater);
                nodeStack.Push(currentNode);
            }
            
            currentNode = repeater;
            isBuilding = true;
            return this;
        }

        /// <summary>
        /// End the current node and return to parent
        /// </summary>
        /// <returns>BTBuilder instance for fluent API</returns>
        public BTBuilder End()
        {
            if (nodeStack.Count > 0)
            {
                currentNode = nodeStack.Pop();
            }
            else
            {
                isBuilding = false;
            }
            
            return this;
        }

        /// <summary>
        /// Complete the tree building and return the root node
        /// </summary>
        /// <returns>Root IBTNode of the built tree</returns>
        public IBTNode Complete()
        {
            if (currentNode == null)
            {
                throw new InvalidOperationException("No nodes have been added to the tree.");
            }
            
            // Clear the stack and return the root node
            nodeStack.Clear();
            isBuilding = false;
            var rootNode = currentNode;
            currentNode = null;
            
            return rootNode;
        }

        /// <summary>
        /// Check if the builder is currently building a tree
        /// </summary>
        /// <returns>True if building, false otherwise</returns>
        public bool IsBuilding()
        {
            return isBuilding;
        }
    }
}

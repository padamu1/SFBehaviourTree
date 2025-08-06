using System;
using UnityEngine;

namespace SimulFactory.BehaviourTree.Nodes
{
    public class BTWait : IBTNode
    {
        readonly string nodeName;
        readonly float waitTime;
        private float startTime;
        private bool isWaiting;

        public BTWait(string nodeName, float waitTime)
        {
            this.nodeName = nodeName;
            this.waitTime = waitTime;
            this.startTime = 0f;
            this.isWaiting = false;
        }

        public void AddChild(IBTNode child)
        {
            throw new System.NotImplementedException("BTWait does not support adding children.");
        }

        public NodeState Run(BTContext btContext)
        {
            if (!isWaiting)
            {
                startTime = Time.time;
                isWaiting = true;
            }

            if (Time.time - startTime >= waitTime)
            {
                isWaiting = false;
                return NodeState.Success;
            }

            return NodeState.Running;
        }
    }
} 
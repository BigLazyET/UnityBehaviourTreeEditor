using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BigLazyET.BT
{
    [System.Serializable]
    public class RandomCOSelector : COSelector
    {
        [Tooltip("Seed the random number generator to make things easier to debug")]
        public int seed = 0;
        [Tooltip("Do we want to use the seed?")]
        public bool useSeed = false;

        protected List<int> childIndexList = new List<int>();
        protected Stack<int> childrenExecutionOrder = new Stack<int>();

        public override void OnInit()
        {
            base.OnInit();

            // If specified, use the seed provided.
            if (useSeed)
                Random.InitState(seed);
        }

        protected override void OnStart()
        {
            // Add the index of each child to a list to make the Fischer-Yates shuffle possible.
            childIndexList.Clear();
            childrenExecutionOrder.Clear();
            for (int i = 0; i < children.Count; ++i)
            {
                childIndexList.Add(i);
            }
            ShuffleChilden();
        }

        protected override State OnUpdate()
        {
            while (childrenExecutionOrder.Any())
            {
                var pickIndex = childrenExecutionOrder.Pop();
                var state = children[pickIndex].Update();
                if (state == State.Running)
                {
                    childrenExecutionOrder.Push(pickIndex);
                    return State.Running;
                }
                else if (state == State.Success)
                {
                    childIndexList.Clear();
                    childrenExecutionOrder.Clear();
                    return State.Success;
                }
            }

            childIndexList.Clear();
            childrenExecutionOrder.Clear();
            return State.Failure;
        }

        protected virtual void ShuffleChilden()
        {
            // Use Fischer-Yates shuffle to randomize the child index order.
            for (int i = childIndexList.Count; i > 0; --i)
            {
                int j = Random.Range(0, i);
                int index = childIndexList[j];
                childrenExecutionOrder.Push(index);
                childIndexList[j] = childIndexList[i - 1];
                childIndexList[i - 1] = index;
            }
        }
    }
}

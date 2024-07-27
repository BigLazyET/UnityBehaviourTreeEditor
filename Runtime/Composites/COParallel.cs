using System.Linq;
using TheKiwiCoder;

namespace BigLazyET.BT
{
    /// <summary>
    /// Carray On from the last running node in Parallel
    /// </summary>
    [System.Serializable]
    public class COParallel : Parallel
    {
        private int currentIndex = 0;
        private State[] executionStatus;

        protected override void OnStart()
        {
            base.OnStart();

            executionStatus = new State[children.Count];
        }

        protected override State OnUpdate()
        {
            while (currentIndex < children.Count)
            {
                var childCount = children.Count;

                for (int i = currentIndex; i < childCount; ++i)
                {
                    var childState = children[i].Update();
                    executionStatus[i] = childState;
                    if (childState != State.Running)
                    {
                        currentIndex++;
                    }
                }

                int successCount = executionStatus.Count(s => s == State.Success);
                int failureCount = executionStatus.Count(s => s == State.Failure);

                if (successCount >= successThreshold)
                {
                    currentIndex = 0;
                    executionStatus = new State[children.Count];
                    return State.Success;
                }

                if (failureCount > (childCount - successThreshold))
                {
                    currentIndex = 0;
                    executionStatus = new State[children.Count];
                    return State.Failure;
                }

                return State.Running;
            }

            currentIndex = 0;
            executionStatus = new State[children.Count];
            return State.Running;
        }
    }
}

using TheKiwiCoder;

namespace BigLazyET.BT
{
    /// <summary>
    /// Carray On from the last running node in Sequencer
    /// </summary>
    [System.Serializable]
    public class COSequencer : Sequencer
    {
        private int currentIndex = 0;

        protected override State OnUpdate()
        {
            while (currentIndex < children.Count)
            {
                var childStatus = children[currentIndex].Update();

                if (childStatus == State.Running)
                {
                    return State.Running;
                }
                else if (childStatus == State.Failure)
                {
                    currentIndex = 0;
                    return State.Failure;
                }
                currentIndex++;
            }

            currentIndex = 0;
            return State.Success;
        }
    }
}

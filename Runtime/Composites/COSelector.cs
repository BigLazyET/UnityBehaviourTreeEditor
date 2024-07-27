using TheKiwiCoder;

namespace BigLazyET.BT
{
    /// <summary>
    /// Carray On from the last running node in Selector
    /// </summary>
    [System.Serializable]
    public class COSelector : Selector
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
                else if (childStatus == State.Success)
                {
                    currentIndex = 0;
                    return State.Success;
                }
                currentIndex++;
            }

            currentIndex = 0;
            return State.Failure;
        }
    }
}
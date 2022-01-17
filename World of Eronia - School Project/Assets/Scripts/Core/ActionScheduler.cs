using UnityEngine;

namespace WorldOfEronia.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;
        public void startAction(IAction action)
        {
            if (currentAction == action) return;
            if (currentAction != null)
            {
                currentAction.Cancel();
                Debug.Log("Cancelling " + currentAction);
            }
            currentAction = action;
        }
    }
}

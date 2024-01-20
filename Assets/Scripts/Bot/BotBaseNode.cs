using UnityEngine;

public abstract class BaseBotNode<T> : ScriptableObject where T : Character
{
    public abstract BehaviourTreeStatus ProcessUpdate(T character);
}

public enum BehaviourTreeStatus
{
    FAILURE,
    RUNNING,
    SUCCESS
}

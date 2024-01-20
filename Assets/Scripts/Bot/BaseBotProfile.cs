using UnityEngine;

public class BaseBotProfile<T> : ScriptableObject where T : Character
{
    public BaseBotNode<T> rootAction;
}

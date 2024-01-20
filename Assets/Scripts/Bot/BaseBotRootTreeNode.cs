

public abstract class BaseBotRootTreeNode<T> : BaseBotNode<T> where T : Character
{
    public abstract void AddChild(BaseBotNode<T> child);
}
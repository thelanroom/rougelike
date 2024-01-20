using System.Collections.Generic;

public class BotActionSelector<T> : BaseBotRootTreeNode<T> where T : Character
{
    public List<BaseBotNode<T>> actions;

    public override BehaviourTreeStatus ProcessUpdate(T character)
    {
        for (int index = 0; index < actions.Count; index++)
        {
            var status = actions[index].ProcessUpdate(character);
            if (status != BehaviourTreeStatus.FAILURE)
            {
                return status;
            }
        }
        return BehaviourTreeStatus.FAILURE;
    }

    public override void AddChild(BaseBotNode<T> child)
    {
        actions.Add(child);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckPlayerInput : BaseBotNode<Character>
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if( character.MoveInput == Vector2.zero)
        {
            return BehaviourTreeStatus.FAILURE;
        }
        return BehaviourTreeStatus.SUCCESS;
    }
}

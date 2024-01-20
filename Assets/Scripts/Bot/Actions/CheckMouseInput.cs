using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckMouseInput : BaseBotNode<Character>
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        return character.AttackDirection != null ? BehaviourTreeStatus.SUCCESS : BehaviourTreeStatus.FAILURE;
    }
}

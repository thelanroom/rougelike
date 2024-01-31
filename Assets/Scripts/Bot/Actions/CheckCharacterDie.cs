using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckCharacterDie : BaseBotNode<Character>
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        return character.CurrentHP <= 0 ? BehaviourTreeStatus.SUCCESS : BehaviourTreeStatus.FAILURE;
    }
}

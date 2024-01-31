using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CheckDamageReceived : BaseBotNode<Character>
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        return character.DamageReceived != 0 ? BehaviourTreeStatus.SUCCESS : BehaviourTreeStatus.FAILURE;
    }
}

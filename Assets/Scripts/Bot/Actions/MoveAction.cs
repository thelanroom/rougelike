using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MoveAction : AnimatedAction
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        character.SetCharacterState(charaterState, animState);
        character.Move();
        return BehaviourTreeStatus.SUCCESS;
    }
}

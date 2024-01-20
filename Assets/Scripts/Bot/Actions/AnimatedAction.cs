using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AnimatedAction : BaseBotNode<Character>
{
    public string animState;
    public CharacterState charaterState;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        character.SetCharacterState(charaterState, animState);
        return BehaviourTreeStatus.SUCCESS;
    }
}

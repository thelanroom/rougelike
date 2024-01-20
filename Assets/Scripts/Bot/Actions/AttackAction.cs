using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackAction : AnimatedAction
{
    public Skill skill;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if(character.CurrentState != charaterState)
        {
            character.UseSkill(skill, animState);
            return BehaviourTreeStatus.SUCCESS;
        }
        return BehaviourTreeStatus.RUNNING;
    }
}

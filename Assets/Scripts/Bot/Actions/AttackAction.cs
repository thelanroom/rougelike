using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class AttackAction : AnimatedAction
{
    public Skill skill;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if(character.CurrentState != charaterState)
        {
            if (character.SkillInCooldown(skill.name)) return BehaviourTreeStatus.FAILURE;

            character.transform.rotation = Quaternion.LookRotation((Vector3)character.AttackDirection);
            character.Weapon.EnableDealDamge(skill.Damage);
            character.SetCharacterState(charaterState, animState);
            character.RegisterSkillCooldown(skill.name, skill.Cooldown);
            return BehaviourTreeStatus.SUCCESS;
        }

        return BehaviourTreeStatus.RUNNING;
    }
}

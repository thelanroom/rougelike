using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackAction : AttackAction
{
    public GameObject projectile;


    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if (character.CurrentState != charaterState)
        {
            character.UseSkill(skill, animState);
            return BehaviourTreeStatus.SUCCESS;
        }
        return BehaviourTreeStatus.RUNNING;
    }
}

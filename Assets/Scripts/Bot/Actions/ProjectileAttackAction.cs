using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ProjectileAttackAction : AttackAction
{
    public ProjectileWeapon weapon;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {

        if (character.CurrentState != charaterState)
        {
            if (character.SkillInCooldown(skill.name)) return BehaviourTreeStatus.FAILURE;

            character.SetCharacterState(charaterState,animState);
            for(int i = -1; i < 2; i++)
            {
                //TODO: use object pool
                var wep = Instantiate(weapon, character.transform.position, Quaternion.identity);
                wep.EnableDealDamge(skill.Damage);
                wep.Init(GetDirection(character.transform.forward, i * 50));
            }

            character.RegisterSkillCooldown(skill.name, skill.Cooldown);
            return BehaviourTreeStatus.SUCCESS;
        }

        return BehaviourTreeStatus.RUNNING;
    }

    private Vector3 GetDirection(Vector3 origin, float angle)
    {
        return  Quaternion.AngleAxis(angle, Vector3.up) * origin;
    }
}

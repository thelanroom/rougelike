using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Burst.CompilerServices;
using UnityEngine;

[CreateAssetMenu]
public class CheckAttackRange : BaseBotNode<Character>
{
    public int range = 1;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if (character.AttackDirection != null) return BehaviourTreeStatus.SUCCESS;

        if(Vector3.Distance(character.transform.position, character.target.position) < range)
        {
            character.AttackDirection = character.target.position - character.transform.position;
            return BehaviourTreeStatus.SUCCESS;
        }

        return BehaviourTreeStatus.FAILURE;
    }


}

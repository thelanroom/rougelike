using UnityEngine;

[CreateAssetMenu]
public class CheckAttackRange : BaseBotNode<Character>
{
    public int range = 1;

    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {
        if (character.target.CurrentState == CharacterState.Die ) return BehaviourTreeStatus.FAILURE;

        if (character.AttackDirection != null) return BehaviourTreeStatus.SUCCESS;

        if (Vector3.Distance(character.transform.position, character.target.transform.position)  < range)
        {
            character.AttackDirection = character.target.transform.position - character.transform.position;
            return BehaviourTreeStatus.SUCCESS;
        }

        return BehaviourTreeStatus.FAILURE;
    }
}

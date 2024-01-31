using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class ChaseAction : BaseBotNode<Character>
{
    public override BehaviourTreeStatus ProcessUpdate(Character character)
    {

        if (character.target == null || character.target.CurrentState == CharacterState.Die)
        {
            character.MoveInput = Vector2.zero;
            return BehaviourTreeStatus.FAILURE;
        }

        if (character.target != null)
        {
            var distance = new Vector2(character.target.transform.position.x - character.transform.position.x,
                                       character.target.transform.position.z - character.transform.position.z);
            float angle = Vector2.SignedAngle(distance, Vector2.up);
            float x = 0;
            float y = 0;

            if (angle >= 0 && angle < 90)
            {
                x = angle / 90;
                y = 1 - x;
            }

            else if (angle >= 90 && angle < 180)
            {
                y = (angle - 90) / -90;
                x = 1 + x;
            }

            else if (angle < 0 && angle >= -90)
            {
                x = angle / 90;
                y = 1 + x;
            }

            else if (angle < -90)
            {
                y = (angle + 90) / 90;
                x = -1 - y;
            }
            character.MoveInput = new Vector2(x, y);

            return BehaviourTreeStatus.SUCCESS;
        }

        return BehaviourTreeStatus.FAILURE;
    }
}

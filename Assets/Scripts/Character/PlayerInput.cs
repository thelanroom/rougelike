using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Vector2 _moveInput;
    private Character _character;

    private void Awake()
    {
       _character = GetComponent<Character>();
    }

    private void Update()
    {
       ReadKeyboardInput();
       ReadMouseInput();
    }

    private void ReadKeyboardInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _moveInput.x = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _moveInput.x = -1;
        }
        else
        {
            _moveInput.x = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _moveInput.y = -1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _moveInput.y = 1;
        }
        else
        {
            _moveInput.y = 0;
        }
        _character.MoveInput = _moveInput;
    }

    private void ReadMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point;
                Vector3 direction = targetPosition - transform.position;
                _character.AttackDirection = new Vector3(direction.x, 0, direction.z);
            }
        }
    }    
}

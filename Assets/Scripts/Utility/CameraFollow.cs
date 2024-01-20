using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private Vector3 _offset;

    private void Start()
    {
        if(target == null)
        {
            target = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        }
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        transform.position = target.position + _offset;
    }
}

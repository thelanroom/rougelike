using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public float speed = 10f;
    public float rotationSpeed = 50f;
    public float liveTimeInSec = 2f;

    [SerializeField] private Transform _origin;
    public Vector3 Direction {  get; private set; }
    private float _elapsedTime;
    
    public void Init(Vector3 direction)
    {
        Direction = direction;
        _elapsedTime = 0;
    }

    private void Update()
    {
        if (Direction == Vector3.zero) return;

        transform.Translate(speed * Time.deltaTime * Direction);
        _origin.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= liveTimeInSec)
        {
            ReturnToPool();
        }
    }

    private void ReturnToPool()
    {
        Direction = Vector3.zero;
        Destroy(gameObject);
    }
}

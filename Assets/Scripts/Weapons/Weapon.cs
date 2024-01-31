using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool _canDealDamage;
    private float _damage;

    private CollisionTrigger _collisionTrigger;

    private void Awake()
    {
        _collisionTrigger  = GetComponentInChildren<CollisionTrigger>();
        _collisionTrigger.onTrigger = HandleCollisionTrigger;
    }

    public void EnableDealDamge(float damage)
    {
        _canDealDamage = true;
        _damage = damage;
    }

    private void HandleCollisionTrigger(Collider other)
    {
        if(_canDealDamage && other.TryGetComponent<Character>(out var ch))
        {
            ch.GetHit(_damage);
            _canDealDamage = false;
        }
    }
}

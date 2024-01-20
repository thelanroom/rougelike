using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private bool _canDealDamage;
    private float _damage;

    public void EnableDealDamge(float damage)
    {
        _canDealDamage = true;
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_canDealDamage && other.TryGetComponent<Character>(out var ch))
        {
            ch.GetHit(_damage);
            _canDealDamage = false;
        }
    }
}

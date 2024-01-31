using System;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
   public Action<Collider> onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger?.Invoke(other);
    }
}

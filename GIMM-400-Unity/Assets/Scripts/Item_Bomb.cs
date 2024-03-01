using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bomb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            var damageable = other.GetComponentInParent<IDamageable>();
            damageable.TakeDamage(10f);
    }
}

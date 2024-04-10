using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpinPad : MonoBehaviour
{

    private Collider collider;

    public void Start()
    {
        collider = GetComponent<Collider>();
        DisableBomb();
        Invoke(nameof(EnableBomb), 1f);
    }

    public void DisableBomb()
    {
        collider.enabled = false;
    }

    public void EnableBomb()
    {
        collider.enabled = true;
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponentInParent<IDamageable>();
        damageable?.TakeDamage(10f);
    }
}
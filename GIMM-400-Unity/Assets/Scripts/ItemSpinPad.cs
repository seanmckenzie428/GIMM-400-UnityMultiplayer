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
        damageable?.TakeDamage(10f); // Damage the object

        if (other.CompareTag("Robot")) // Check if collided with a robot
        {
            LaunchPlayer(other.transform.root.gameObject); // Launch the player
        }
    }

    private void LaunchPlayer(GameObject player)
    {
        var arcadeKart = player.GetComponent<ArcadeKart>();
        if (arcadeKart != null)
        {
            // Launch straight up
            var launchDirection = Vector3.up;

            // Apply a force in the launch direction
            arcadeKart.Rigidbody.AddForce(launchDirection * arcadeKart.launchForce, ForceMode.Impulse);
        }
    }
}
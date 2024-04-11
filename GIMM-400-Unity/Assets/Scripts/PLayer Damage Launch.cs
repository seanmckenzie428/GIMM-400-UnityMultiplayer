using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class PLayerDamageLaunch : MonoBehaviour, IDamageable
{
       private ArcadeKart arcadeKart;
    public float launchForce = 10f;
    public string targetAITag = "Robot"; // Tag of the AI object to trigger the launch

    private void Start()
    {
        arcadeKart = GetComponent<ArcadeKart>();
    }

    public void TakeDamage(float damage)
    {
        // StopVelocity();
        arcadeKart.Rigidbody.velocity = arcadeKart.Rigidbody.velocity / 2;
        InvokeRepeating(nameof(Launch), 0, 0.1f);
        Invoke(nameof(CancelLaunch), 1f);
    }

    private void StopVelocity()
    {
        arcadeKart.Rigidbody.velocity = Vector3.zero;
    }

    private void CancelLaunch()
    {
        CancelInvoke(nameof(Launch));
    }

    private void Launch()
    {
        // Launch straight up
        var launchDirection = Vector3.up;

        // Apply a force in the launch direction
        arcadeKart.Rigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if collided with AI object
        if (collision.collider.CompareTag(targetAITag))
        {
            // Trigger the launch behavior
            Launch();
        }
    }
}
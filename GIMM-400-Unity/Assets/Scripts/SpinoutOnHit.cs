using KartGame.KartSystems;
using UnityEngine;

[RequireComponent(typeof(ArcadeKart))]
public class SpinoutOnHit : MonoBehaviour, IDamageable
{
    private float damageToForceRatio = 20f;
    private float spinOutDuration = 2.0f; // Duration of spin effect
    private bool isSpinningOut = false;
    private float spinEndTime = 0;
    private float spinOutForce = 0;

    private ArcadeKart kart;

    void Start()
    {
        kart = GetComponent<ArcadeKart>();
    }

    public void TakeDamage(float damage = 0)
    {
        isSpinningOut = true;
        spinOutForce = damage * damageToForceRatio;
        spinEndTime = Time.time + spinOutDuration;
    }

    void Update()
    {
        if (isSpinningOut)
        {
            // Apply the spin-out force at the position of one of the rear wheels.
            Vector3 forcePosition = kart.RearRightWheel.transform.position;
            Vector3 forceDirection = kart.transform.right;
            kart.Rigidbody.AddForceAtPosition(forceDirection * spinOutForce, forcePosition);

            // Stop applying force after the specified duration
            if (Time.time >= spinEndTime)
            {
                isSpinningOut = false;
            }
        }
    }
}
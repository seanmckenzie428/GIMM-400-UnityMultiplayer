using KartGame.KartSystems;
using UnityEngine;

[RequireComponent(typeof(ArcadeKart))]
public class PlayerDamageable : MonoBehaviour, IDamageable
{
    private ArcadeKart arcadeKart;
    public float spinoutForce = 10f;
    public float spinoutTorque = 10f;

    private void Start()
    {
        arcadeKart = GetComponent<ArcadeKart>();
    }

    public void TakeDamage(float damage)
    {
        // StopVelocity();
        arcadeKart.Rigidbody.velocity = arcadeKart.Rigidbody.velocity / 2;
        InvokeRepeating(nameof(Spinout), 0, 0.1f);
        Invoke(nameof(CancelSpinout), 1f);
    }

    private void StopVelocity()
    {
        arcadeKart.Rigidbody.velocity = Vector3.zero;
    }

    private void CancelSpinout()
    {
        CancelInvoke(nameof(Spinout));
    }

    private void Spinout()
    {
        var spinoutDirection =
            // Check if the kart's velocity is near zero
            // If so, apply the force in a random direction
            arcadeKart.Rigidbody.velocity.magnitude < 0.1f ? Random.onUnitSphere :
            // Otherwise, calculate the spinout direction as before
            Vector3.Cross(arcadeKart.Rigidbody.velocity, Vector3.up).normalized;

        // Apply a force in the spinout direction
        arcadeKart.Rigidbody.AddForce(spinoutDirection * spinoutForce, ForceMode.Impulse);

        // Apply a torque to make the car spin
        arcadeKart.Rigidbody.AddTorque(Vector3.up * spinoutTorque, ForceMode.Impulse);
    }
}
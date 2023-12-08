using KartGame.KartSystems;
using UnityEngine;

public class TiltBike : MonoBehaviour
{

    public float tiltAnimationDamping = 5f;
    [Space]
    [Tooltip("The maximum angle in degrees that the bike will tilt, visually. This does not affect the actual steering angle.")]
    public float maxTiltAngle = 20f;

    private Transform t;
    private ArcadeKart kartController;
    private float m_SmoothedSteeringInput;

    void Start()
    {
        t = transform;
        kartController = GetComponentInParent<ArcadeKart>();
    }

    void FixedUpdate()
    {
        // Uses the same logic as the kart wheels to steer, but uses the value to tile the bike instead of turning the wheels
        m_SmoothedSteeringInput = Mathf.MoveTowards(m_SmoothedSteeringInput, kartController.Input.TurnInput,
            tiltAnimationDamping * Time.deltaTime);

        float rotationAngle = m_SmoothedSteeringInput * maxTiltAngle;

        var r = t.rotation.eulerAngles;
        r.z = -1 * rotationAngle;
        t.rotation = Quaternion.Euler(r);
    }


}

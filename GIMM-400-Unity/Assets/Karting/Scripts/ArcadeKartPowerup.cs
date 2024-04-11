using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.Events;

public class ArcadeKartPowerup : MonoBehaviour {

    public ArcadeKart.StatPowerup boostStats = new ArcadeKart.StatPowerup
    {
        MaxTime = 5
    };

    public bool isCoolingDown { get; private set; }
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown = 5f;
    public float respawnDelay = 3f; // Added field for respawn delay

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinishCooldown;

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;
    }

    private void Update()
    {
        if (isCoolingDown) { 
            if (Time.time - lastActivatedTimestamp > cooldown) {
                //finished cooldown!
                isCoolingDown = false;
                onPowerupFinishCooldown.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        var rb = other.attachedRigidbody;
        if (rb) {
            var kart = rb.GetComponent<ArcadeKart>();
            if (kart)
            { 
                lastActivatedTimestamp = Time.time;
                kart.AddPowerup(this.boostStats);
                onPowerupActivated.Invoke();
                isCoolingDown = true;

                if (disableGameObjectWhenActivated) 
                    this.gameObject.SetActive(false);
                
                // Invoke respawn after delay
                Invoke("RespawnPowerup", respawnDelay);
            }
        }
    }

    // Respawn function to reset powerup status
    private void RespawnPowerup()
    {
        isCoolingDown = false;
        lastActivatedTimestamp = -9999f;
        if (disableGameObjectWhenActivated)
            this.gameObject.SetActive(true);
    }
}

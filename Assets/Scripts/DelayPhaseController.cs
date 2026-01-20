using UnityEngine;

public class DelayPhaseController : MonoBehaviour
{
    [Header("Delay Settings")]
    public float slowMultiplier = 0.3f;

    private float timer;
    private float maxDuration;
    private bool active;

    private PlayerMovement movement;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (!active) return;

        timer += Time.deltaTime;

        if (timer >= maxDuration)
        {
            EndDelay();
        }
    }

    // ========================
    // PUBLIC API
    // ========================
    public void TriggerDelay(float duration)
    {
        // Extend delay safely
        maxDuration = Mathf.Max(maxDuration, duration);

        if (active) return;

        active = true;
        timer = 0f;

        if (movement != null)
            movement.SetMoveMultiplier(slowMultiplier);
    }

    // ========================
    // CLEAN EXIT
    // ========================
    void EndDelay()
    {
        active = false;
        timer = 0f;
        maxDuration = 0f;

        if (movement != null)
            movement.SetMoveMultiplier(1f);
    }

    public bool IsDelayActive()
    {
        return active;
    }

    // ========================
    // FAILSAFE
    // ========================
    void OnDisable()
    {
        // Prevent permanent slow if object is disabled
        if (movement != null)
            movement.SetMoveMultiplier(1f);
    }
}

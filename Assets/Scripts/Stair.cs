using UnityEngine;

public class Stair : MonoBehaviour
{
    public float stairSlope = 0.4f;
    public bool rightIsUp = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var pm = other.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.EnterStairs(stairSlope, rightIsUp);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var pm = other.GetComponent<PlayerMovement>();
        if (pm != null)
            pm.ExitStairs();
    }
}

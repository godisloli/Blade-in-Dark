using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    private Transform player;
    private bool collected = false;

    public AudioClip healSound;
    public int amount = 20;
    public float collectDistance = 0.2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null || collected)
            return;

        // Safety auto-collect (in case trigger misses)
        if (Vector2.Distance(transform.position, player.position) <= collectDistance)
        {
            player.GetComponent<EntityStat>()?.Heal(amount);
            GlobalSound.Instance?.PlaySFX(healSound);
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<EntityStat>()?.Heal(amount);
            GlobalSound.Instance?.PlaySFX(healSound);
            Object.Destroy(gameObject);
        }
    }
}

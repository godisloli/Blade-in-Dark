using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isHostile = false;
    public bool isEnemy = false;
    public bool isAlly = false;
    public bool isPlayer = false;
    private Ihurtable hurtableComponent;

    private EntityStat entityStat;
    void Start()
    {
        entityStat = GetComponent<EntityStat>();
        hurtableComponent = GetComponent<Ihurtable>();
    }

    public void Hurt(int damage)
    {
        if (!isDead())
        {
            entityStat.TakeDamage(damage);
            hurtableComponent?.OnHurt();
        }
            
    }

    public void Hurt(int damage, Vector2 knockbackDirection, float knockbackStrength)  
    {
        if (!isDead())
        {
            entityStat.TakeDamage(damage);
            hurtableComponent?.OnHurt();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode2D.Impulse);
            }
        }
    }

    public void Hurt(int damage, float delay)
    {
        if (!isDead())
        {
            entityStat.TakeDamage(damage);
            hurtableComponent?.OnHurt();
            DelayPhaseController delayPhase = GetComponent<DelayPhaseController>();
            if (delayPhase != null)
            {
                delayPhase.TriggerDelay(delay);
            }
        }
    }

    public void Hurt(int damage, Vector2 knockbackDirection, float knockbackStrength, float delay)  
    {
        if (!isDead())
        {
            entityStat.TakeDamage(damage);
            hurtableComponent?.OnHurt();
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            DelayPhaseController delayPhase = GetComponent<DelayPhaseController>();
            if (rb != null)
            {
                rb.AddForce(knockbackDirection.normalized * knockbackStrength, ForceMode2D.Impulse);
                delayPhase?.TriggerDelay(delay);
            }
        }
    }

    public void heal(int amount)
    {
        entityStat.Heal(amount);
    }

    public bool isDead()
    {
        return entityStat.currentHP <= 0;
    }
}

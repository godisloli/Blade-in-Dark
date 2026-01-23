using System;
using UnityEngine;

public class EntityStat : MonoBehaviour
{
    public static event Action<EntityStat> OnEntityDied;
    public bool isPlayer;

    [Header("Health")]
    public int maxHP = 100;
    public int currentHP;
    public Healthbar healthbar;
    public AudioClip EntityDead;

    [Header("Combat")]
    public int attack = 10;
    public float attackRange = 1.5f;
    public int defense = 2;
    public float knockbackStrength = 1f;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("State")]
    public bool isDead;

    private Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        healthbar?.setMaxHealth(maxHP);
    }

    private void Update()
    {
        healthbar?.setMaxHealth(maxHP);
        healthbar?.setHealth(currentHP);
    }
    public void TakeDamage(int rawDamage)
    {
        if (isDead) return;

        healthbar?.Shake();
        int finalDamage = Mathf.Max(rawDamage - defense, 1);
        currentHP -= finalDamage;

        if (currentHP <= 0)
        { 
            anim.SetTrigger("die");
            Die();
        }              
    }
    public void Heal(int amount)
    {
        if (isDead) return;

        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }
    void Die()
    {
        isDead = true;
        GlobalSound.Instance?.PlaySFX(EntityDead);
        OnEntityDied?.Invoke(this);
        anim.SetBool("isDead", true);
        GetComponent<Lootable>()?.DropLoot();
        UnityEngine.Object.FindFirstObjectByType<LevelManager>()?.RegisterEnemyKill();
        UnityEngine.Object.Destroy(gameObject, 1f);
    }
}

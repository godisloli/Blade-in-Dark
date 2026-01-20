using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    public float attackRange = 2.5f;
    public float attackAngle = 60f;

    [Header("Delay Phase")]
    public float delayOnAttack = 0.25f;

    [Header("Failsafe")]
    public float maxAttackLockTime = 1.2f; // MUST be longer than animations

    private Animator anim;
    private DelayPhaseController delay;

    private bool isAttacking;
    private bool canQueueCombo;
    private bool comboQueued;
    private int comboStep;
    private LivingEntity livingEntity;
    private LivingEntity targetEntity;
    private EntityStat entityStat;

    private float attackTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        delay = GetComponent<DelayPhaseController>();
        livingEntity = GetComponent<LivingEntity>();
        entityStat = GetComponent<EntityStat>();
    }

    void Update()
    {
        // FAILSAFE — prevent permanent lock
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= maxAttackLockTime)
            {
                ForceResetCombat();
            }
        }

        if (Input.GetMouseButtonDown(0))
            RegisterAttackInput();
    }

    // ========================
    // INPUT
    // ========================
    void RegisterAttackInput()
    {
        if (!isAttacking && !comboQueued)
        {
            StartAttack1();
            return;
        }

        if (canQueueCombo)
        {
            comboQueued = true;
            anim.SetBool("comboQueued", true);
        }
    }

    // ========================
    // ATTACK LOGIC
    // ======================== 
    void StartAttack1()
    {
        isAttacking = true;
        comboStep = 1;
        comboQueued = false;
        canQueueCombo = false;
        attackTimer = 0f;

        anim.SetBool("attack", true);
        anim.SetInteger("comboStep", 1);
        anim.SetBool("isAttacking", true);
        anim.SetBool("comboQueued", false);

        delay?.TriggerDelay(delayOnAttack);
    }

    void StartAttack2()
    {
        comboStep = 2;
        comboQueued = false;
        canQueueCombo = false;
        attackTimer = 0f;

        anim.SetBool("attack", true);
        anim.SetInteger("comboStep", 2);
        anim.SetBool("comboQueued", false);

        delay?.TriggerDelay(delayOnAttack);
    }

    // ========================
    // ANIMATION EVENTS
    // ========================

    // Mid Attack_1
    public void EnableComboQueue()
    {
        canQueueCombo = true;
    }

    public void AttackHit_1()
    {
        DoConeAttack();
    }

    public void AttackHit_2()
    {
        DoConeAttack();
    }

    // LAST FRAME Attack_1
    public void AttackEnd_1()
    {
        if (comboQueued)
        {
            StartAttack2();
        }
        else
        {
            ResetCombat();
        }
    }

    // LAST FRAME Attack_2
    public void AttackEnd_2()
    {
        ResetCombat();
    }

    // ========================
    // RESET (SAFE)
    // ========================
    void ResetCombat()
    {
        isAttacking = false;
        comboQueued = false;
        canQueueCombo = false;
        comboStep = 0;
        attackTimer = 0f;
        anim.SetBool("attack", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("comboQueued", false);
        anim.SetInteger("comboStep", 0);
    }

    void ForceResetCombat()
    {
        Debug.LogWarning("Attack force-reset (failsafe)");
        ResetCombat();
    }

    // ========================
    // DAMAGE
    // ========================
    void DoConeAttack()
    {
        Vector2 dir = GetMouseDirection();

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (Collider2D hit in hits)
        {
            LivingEntity target = hit.GetComponent<LivingEntity>();
            if (target == null) continue;

            if (target.isEnemy == livingEntity.isEnemy) continue;

            Vector2 toTarget =
                ((Vector2)hit.transform.position - (Vector2)transform.position).normalized;

            float angle = Vector2.Angle(dir, toTarget);

            if (angle <= attackAngle * 0.5f)
            {
                Vector2 knockbackDir = toTarget;

                target.Hurt(
                    entityStat.attack,
                    knockbackDir,
                    entityStat.knockbackStrength,
                    delayOnAttack
                );
            }
        }
    }

    Vector2 GetMouseDirection()
    {
        Vector3 mouse =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        return (mouse - transform.position).normalized;
    }
}

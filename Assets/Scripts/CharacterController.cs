using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private float moveMultiplier = 1f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Camera cam;
    private DelayPhaseController delay;

    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        delay = GetComponent<DelayPhaseController>();
    }

    void Update()
    {
        ReadInput();
        UpdateAnimation();
        HandleFacing();
    }

    void FixedUpdate()
    {
        Move();
    }

    // -----------------------------
    // INPUT
    // -----------------------------
    void ReadInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;
    }

    // -----------------------------
    // MOVEMENT
    // -----------------------------
    void Move()
    {
        rb.MovePosition(
            rb.position + input * moveSpeed * moveMultiplier * Time.fixedDeltaTime
        );
    }

    // -----------------------------
    // ANIMATION
    // -----------------------------
    void UpdateAnimation()
    {
        anim.SetBool("isWalking", input.sqrMagnitude > 0.01f);
    }

    // -----------------------------
    // MOUSE FACING (PLAYER AS PIVOT)
    // -----------------------------
    void HandleFacing()
    {
        // Lock facing during delay phase
        if (delay != null && delay.IsDelayActive())
            return;

        Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        float deltaX = mouseWorld.x - transform.position.x;

        if (Mathf.Abs(deltaX) < 0.01f)
            return;

        sr.flipX = deltaX < 0f;
    }

    // -----------------------------
    // CALLED BY DelayPhaseController
    // -----------------------------
    public void SetMoveMultiplier(float value)
    {
        moveMultiplier = Mathf.Clamp(value, 0f, 1f);
    }
}

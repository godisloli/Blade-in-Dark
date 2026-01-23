using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private int insideCount = 0;

    public AudioClip open;
    public AudioClip close;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsValidOpener(other)) return;

        insideCount++;

        if (insideCount == 1)
        {
            GlobalSound.Instance?.PlaySFX(open);
            anim.SetBool("isOpen", true);
            anim.SetTrigger("Trigger");
        }
            
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!IsValidOpener(other)) return;

        insideCount--;

        if (insideCount <= 0)
        {
            GlobalSound.Instance?.PlaySFX(close);
            insideCount = 0;
            anim.SetBool("isOpen", false);
            anim.SetTrigger("Trigger");
        }
    }

    bool IsValidOpener(Collider2D col)
    {
        return col.CompareTag("Player") || col.CompareTag("Enemy");
    }
}

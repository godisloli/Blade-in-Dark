using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private int insideCount = 0;

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

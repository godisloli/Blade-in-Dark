using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;
    public LootTable lootTable;
    private bool opened = false;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;

        if (other.CompareTag("Player"))
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        opened = true;
        animator?.SetTrigger("Open");
    }

    void SetOpened()
    {
        lootTable?.DropLoot(transform.position);
        animator?.SetBool("IsOpen", true);
    }
}

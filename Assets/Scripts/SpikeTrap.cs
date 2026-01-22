using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    bool active = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var entity = collision.gameObject.GetComponent<LivingEntity>();
        if (active)
        {
            entity.Hurt(20);
        }
    }

    public void SetActive()
    {
        active = true;
    }

    public void SetDisable()
    {
        active = false;
    }
}

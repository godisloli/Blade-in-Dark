using UnityEngine;
using System.Collections;

public class MenuFlyInController : MonoBehaviour
{
    [System.Serializable]
    public class FlyButton
    {
        public Transform button;
        public Vector3 targetPosition;
        public float delay;
    }

    public FlyButton[] buttons;
    public float flySpeed = 6f;
    public float overshoot = 10f;

    public void ShowMenu()
    {
        foreach (var btn in buttons)
        {
            StartCoroutine(FlyIn(btn));
        }
    }

    IEnumerator FlyIn(FlyButton btn)
    {
        yield return new WaitForSeconds(btn.delay);

        Vector3 startPos = btn.button.position;
        Vector3 endPos = btn.targetPosition;
        Vector3 overshootPos = endPos + (endPos - startPos).normalized * overshoot;

        float t = 0f;

        // Fly in
        while (t < 1f)
        {
            t += Time.deltaTime * flySpeed;
            btn.button.position = Vector3.Lerp(startPos, overshootPos, t);
            yield return null;
        }

        // Settle back
        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * flySpeed * 1.5f;
            btn.button.position = Vector3.Lerp(overshootPos, endPos, t);
            yield return null;
        }

        btn.button.position = endPos;
    }
}

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TextBlink : MonoBehaviour
{
    public float blinkSpeed = 1.5f;   // Lower = slower, softer

    private SpriteRenderer sr;
    private float timer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime * blinkSpeed;

        float alpha = Mathf.Lerp(0.3f, 1f, (Mathf.Sin(timer) + 1f) / 2f);

        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}
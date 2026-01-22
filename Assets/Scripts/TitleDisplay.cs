using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TMPTitleFade : MonoBehaviour
{
    [Header("Timings")]
    public float fadeInTime = 1f;
    public float stayTime = 2f;
    public float fadeOutTime = 1f;

    private TMP_Text tmp;
    private Color color;

    void Awake()
    {
        tmp = GetComponent<TMP_Text>();
        color = tmp.color;
        color.a = 0f;
        tmp.color = color;
    }

    void Start()
    {
        StartCoroutine(FadeRoutine());
    }

    IEnumerator FadeRoutine()
    {
        // Fade In
        yield return Fade(0f, 1f, fadeInTime);

        // Wait
        yield return new WaitForSeconds(stayTime);

        // Fade Out
        yield return Fade(1f, 0f, fadeOutTime);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, t / duration);
            tmp.color = color;
            yield return null;
        }

        color.a = to;
        tmp.color = color;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ClickToContinue : MonoBehaviour
{
    public float fadeOutSpeed = 2f;
    public MenuFlyInController menuController;
    public AudioClip sfxClick;

    private SpriteRenderer sr;
    private TextBlink blink;
    private bool clicked = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        blink = GetComponent<TextBlink>();
    }

    void Update()
    {
        if (clicked) return;

        if (Input.GetMouseButtonDown(0))
        {
            clicked = true;
            GlobalSound.Instance?.PlaySFX(sfxClick);
            if (blink != null)
                blink.enabled = false;

            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        float alpha = sr.color.a;

        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeOutSpeed;
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
            yield return null;
        }

        gameObject.SetActive(false);

        menuController.ShowMenu();
    }
}

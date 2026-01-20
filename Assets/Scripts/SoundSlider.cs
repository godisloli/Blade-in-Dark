using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundSlider : MonoBehaviour
{
    public enum Channel
    {
        BGM,
        SFX
    }

    [Header("Audio Channel")]
    public Channel channel;

    [Header("Optional")]
    public AudioClip clickSound;

    private Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        if (GlobalSound.Instance == null)
            return;

        // Set initial value based on channel
        switch (channel)
        {
            case Channel.BGM:
                slider.SetValueWithoutNotify(GlobalSound.Instance.bgmVolume);
                break;

            case Channel.SFX:
                slider.SetValueWithoutNotify(GlobalSound.Instance.sfxVolume);
                break;
        }

        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnValueChanged);
    }

    void OnValueChanged(float value)
    {
        if (GlobalSound.Instance == null)
            return;

        switch (channel)
        {
            case Channel.BGM:
                GlobalSound.Instance.SetBGMVolume(value);
                break;

            case Channel.SFX:
                GlobalSound.Instance.SetSFXVolume(value);
                break;
        }

        if (clickSound != null)
            GlobalSound.Instance.PlaySFX(clickSound);
    }
}

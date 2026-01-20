using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    public static GlobalSound Instance;

    [Header("Volumes")]
    [Range(0f, 1f)] public float bgmVolume = 1f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    public bool muteBGM = false;
    public bool muteSFX = false;

    [Header("Sources")]
    private AudioSource bgmSource;
    private AudioSource sfxSource;
    private AudioSource loopSfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // BGM
        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.spatialBlend = 0f;

        // One-shot SFX
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;

        // Loop SFX (footsteps, hazards, etc.)
        loopSfxSource = gameObject.AddComponent<AudioSource>();
        loopSfxSource.loop = true;
        loopSfxSource.playOnAwake = false;
        loopSfxSource.spatialBlend = 0f;

        LoadSettings();
        ApplyVolumes();
    }

    /* ================= APPLY ================= */

    void ApplyVolumes()
    {
        bgmSource.volume = muteBGM ? 0f : bgmVolume;
        sfxSource.volume = muteSFX ? 0f : sfxVolume;
        loopSfxSource.volume = muteSFX ? 0f : sfxVolume;
    }

    /* ================= BGM ================= */

    public void PlayBGM(AudioClip clip, bool restart = false)
    {
        if (clip == null) return;

        if (bgmSource.clip == clip && bgmSource.isPlaying && !restart)
            return;

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    public void PauseBGM() => bgmSource.Pause();
    public void ResumeBGM() => bgmSource.UnPause();

    /* ================= SFX ================= */

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || muteSFX) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, Vector3 position)
    {
        if (clip == null || muteSFX) return;
        AudioSource.PlayClipAtPoint(clip, position, sfxVolume);
    }

    /* ================= LOOP SFX ================= */

    public void PlayLoopSFX(AudioClip clip)
    {
        if (clip == null || muteSFX) return;

        if (loopSfxSource.clip == clip && loopSfxSource.isPlaying)
            return;

        loopSfxSource.clip = clip;
        loopSfxSource.Play();
    }

    public void StopLoopSFX(AudioClip clip = null)
    {
        if (!loopSfxSource.isPlaying)
            return;

        if (clip != null && loopSfxSource.clip != clip)
            return;

        loopSfxSource.Stop();
        loopSfxSource.clip = null;
    }

    /* ================= SETTINGS ================= */

    public void SetBGMVolume(float value)
    {
        bgmVolume = Mathf.Clamp01(value);
        muteBGM = false;
        ApplyVolumes();
        SaveSettings();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        muteSFX = false;
        ApplyVolumes();
        SaveSettings();
    }

    public void SetMuteBGM(bool mute)
    {
        muteBGM = mute;
        ApplyVolumes();
        SaveSettings();
    }

    public void SetMuteSFX(bool mute)
    {
        muteSFX = mute;
        ApplyVolumes();
        SaveSettings();
    }

    /* ================= SAVE / LOAD ================= */

    void SaveSettings()
    {
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("MuteBGM", muteBGM ? 1 : 0);
        PlayerPrefs.SetInt("MuteSFX", muteSFX ? 1 : 0);
    }

    void LoadSettings()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        muteBGM = PlayerPrefs.GetInt("MuteBGM", 0) == 1;
        muteSFX = PlayerPrefs.GetInt("MuteSFX", 0) == 1;
    }
}

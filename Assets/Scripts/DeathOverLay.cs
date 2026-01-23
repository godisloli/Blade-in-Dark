using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathOverlay : MonoBehaviour
{
    [SerializeField] private Canvas canvasRoot;
    [SerializeField] private GameObject panelRoot;
    public AudioClip deathSound;

    void Awake()
    {
        canvasRoot.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasRoot.sortingOrder = 2000;
        panelRoot.SetActive(false);
    }

    void OnEnable()
    {
        EntityStat.OnEntityDied += OnEntityDied;
    }

    void OnDisable()
    {
        EntityStat.OnEntityDied -= OnEntityDied;
    }

    void OnEntityDied(EntityStat entity)
    {
        if (!entity.isPlayer) return;
        GlobalSound.Instance?.PlaySFX(deathSound);
        panelRoot.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

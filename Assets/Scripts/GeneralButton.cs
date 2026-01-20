using UnityEngine;

public class GeneralButton : MonoBehaviour
{
    public void playSound(AudioClip buttonClickSound)
    {
        GlobalSound.Instance?.PlaySFX(buttonClickSound);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void muteSound(bool isMuted)
    {
        GlobalSound.Instance?.SetMuteSFX(isMuted);
    }

    public void muteBGM(bool isMuted)
    {
        GlobalSound.Instance?.SetMuteBGM(isMuted);
    }
}

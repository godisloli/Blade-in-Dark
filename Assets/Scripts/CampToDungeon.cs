using UnityEngine;
using UnityEngine.SceneManagement;

public class CampToDungeon : MonoBehaviour
{
    public string Level_ID;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(Level_ID);
    }
}

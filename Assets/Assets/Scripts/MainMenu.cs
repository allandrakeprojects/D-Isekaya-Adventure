using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] menuSound;

    public void StartButton()
    {
        AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        SceneManager.LoadScene("Game");
    }

    public void QuitButton()
    {
        AudioSource.PlayClipAtPoint(menuSound[1], Camera.main.transform.position);
        Application.Quit();
    }
}

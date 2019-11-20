using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] menuSound;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    public void StartButton()
    {
        AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        levelManager.LoadGameAfterDelay();
    }

    public void QuitButton()
    {
        AudioSource.PlayClipAtPoint(menuSound[1], Camera.main.transform.position);
        levelManager.LoadQuitAfterDelay();
    }

    public void Resume()
    {
        this.gameObject.SetActive(false);
    }
}

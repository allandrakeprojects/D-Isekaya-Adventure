using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] menuSound;
    private LevelManager levelManager;
    public Text coinCountText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        UpdateScore();
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        }

        levelManager.LoadMainMenuAfterDelay();
    }

    public void StartButton()
    {
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        }

        levelManager.LoadGameAfterDelay();
    }

    public void QuitButton()
    {
        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(menuSound[1], Camera.main.transform.position);
        }

        levelManager.LoadQuitAfterDelay();
    }

    public void Resume()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateScore()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName.Equals("Main_Menu"))
        {
            if (PlayerPrefs.GetInt("highscore", 0) == 0)
            {
                coinCountText.text = "HIGHSCORE: 0";
            }
            else
            {
                coinCountText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("highscore", 0) == 0)
            {
                coinCountText.text = "SCORE: 0";
            }
            else
            {
                if (PlayerPrefs.GetInt("score", 0) == 10000)
                {
                    coinCountText.text = "NEW HIGHSCORE: " + PlayerPrefs.GetInt("highscore");
                }
                else
                {
                    coinCountText.text = "SCORE: " + PlayerPrefs.GetInt("score");
                }

                PlayerPrefs.SetInt("score", 0);
                PlayerPrefs.Save();
            }
        }

    }
}

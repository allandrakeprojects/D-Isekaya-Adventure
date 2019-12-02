using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadMainMenuAfterDelay()
    {
        Invoke("MainMenu", 1.0f);
    }

    public void LoadGameAfterDelay()
    {
        Invoke("Game", 1.0f);
    }

    public void LoadQuitAfterDelay()
    {
        Invoke("Quit", 1.0f);
    }

    public void LoadWinMenuAfterDelay()
    {
        Invoke("WinMenu", 1.0f);
    }

    public void LoadLoseMenuAfterDelay()
    {
        Invoke("LoseMenu", 2.0f);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void WinMenu()
	{
		SceneManager.LoadScene("Win_Screen");
    }

    public void LoseMenu()
    {
        SceneManager.LoadScene("Lose_Screen");
    }
}

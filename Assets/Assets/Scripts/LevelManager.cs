using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGameAfterDelay()
    {
        Invoke("Game", 1.0f);
    }

    public void LoadQuitAfterDelay()
    {
        Invoke("Quit", 1.0f);
    }

    public void LoadLooseLevelAfterDelay()
    {
        Invoke("LoadLoseLevel", 2.0f);
    }

    public void LoadWinLevelAfterDelay()
    {
        Invoke("LoadWinLevel", 2.0f);
    }

    public void Game()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadWinLevel()
	{
		SceneManager.LoadScene("Win_Screen");
    }

    public void LoadLoseLevel()
    {
        SceneManager.LoadScene("Lose_Screen");
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public void LoadLooseLevelAfterDelay()
	{
		Invoke("LoadLoseLevel", 2.0f);
	}

	public void LoadLoseLevel()
	{
		SceneManager.LoadScene("Lose_Screen");
	}

	public void LoadWinLevel()
	{
		SceneManager.LoadScene("Win_Screen");
	}

	public void QuitRequest()
	{
		Application.Quit();
	}


}

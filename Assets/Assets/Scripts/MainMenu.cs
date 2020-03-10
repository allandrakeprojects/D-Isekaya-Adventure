using System;
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
    public Text heartCountText;
    public Text messageHeartText;
    public bool detectStart = false;
    [SerializeField] private GameObject IAPMenu;
    [SerializeField] private GameObject LevelSelection;
    public Button[] LevelBars;
    public Button[] HeroButtons;
    public GameObject about;
    public GameObject hero;
    public GameObject camera;
    public GameObject retry;
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        _anim = GameObject.FindGameObjectWithTag("Fade").GetComponentInChildren<Animator>();

        UpdateScore();
        UpdateLevel();
        SelectedHero();
    }

    private bool isRetryClick = true;

    public void Retry()
    {
        if (isRetryClick)
        {
            isRetryClick = false;

            if (PlayerPrefs.GetInt("Muted") == 0)
            {
                AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
            }

            StartCoroutine(WaitRetry());
        }
    }

    IEnumerator WaitRetry()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerPrefs.SetInt("IS_DIED", 0);
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt("heart") == 0)
        {
            messageHeartText.text = "Not Enough Heart!";
        }
        else
        {
            messageHeartText.text = "";
            int getHeart = PlayerPrefs.GetInt("heart") - 1;
            Debug.Log(getHeart.ToString());
            PlayerPrefs.SetInt("heart", getHeart);
            PlayerPrefs.Save();
            heartCountText.text = getHeart.ToString();

            _anim.SetTrigger("FadeIn");
            retry.SetActive(false);
            isRetryClick = true;
        }
    }

    public void MainMenuButton()
    {
        PlayerPrefs.SetString("waitDate", System.DateTime.Now.AddHours(1).AddMinutes(30).ToString());
        PlayerPrefs.Save();

        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        }

        _anim.SetBool("FadeOut", true);
        levelManager.LoadMainMenuAfterDelay();
    }

    public void PLAYGAME()
    {
        if (PlayerPrefs.GetInt("heart") == 0)
        {
            messageHeartText.text = "Not Enough Heart!";
        }
        else
        {
            if (!detectStart)
            {
                detectStart = true;
                messageHeartText.text = "";
                int getHeart = PlayerPrefs.GetInt("heart") - 1;
                Debug.Log(getHeart.ToString());
                PlayerPrefs.SetInt("heart", getHeart);
                PlayerPrefs.Save();
                Debug.Log(getHeart.ToString());
                heartCountText.text = getHeart.ToString();

                _anim.SetBool("FadeOut", true);
                levelManager.LoadGameAfterDelay();
            }
        }

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(menuSound[0], Camera.main.transform.position);
        }
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

    public void OpenIAP()
    {
        IAPMenu.SetActive(true);
    }

    public void CloseIAP()
    {
        IAPMenu.SetActive(false);
    }

    public void OpenLevelSelection()
    {
        LevelSelection.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        LevelSelection.SetActive(false);
    }

    public void OpenLevel_1()
    {
        PlayerPrefs.SetInt("level_selected", 1);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_2()
    {
        PlayerPrefs.SetInt("level_selected", 2);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_3()
    {
        PlayerPrefs.SetInt("level_selected", 3);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_4()
    {
        PlayerPrefs.SetInt("level_selected", 4);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenLevel_5()
    {
        PlayerPrefs.SetInt("level_selected", 5);
        PlayerPrefs.Save();
        PLAYGAME();
    }

    public void OpenAbout()
    {
        about.SetActive(true);
    }

    public void CloseAbout()
    {
        about.SetActive(false);
    }

    public void OpenHero()
    {
        hero.SetActive(true);
        camera.SetActive(true);
    }

    public void CloseHero()
    {
        hero.SetActive(false);
        camera.SetActive(false);
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

    public void UpdateLevel()
    {
        try
        {
            if (PlayerPrefs.GetString("level", "1,0,0,0,0") == "1,0,0,0,0")
            {
                // Level 1
                PlayerPrefs.SetString("level", "1,0,0,0,0");
                PlayerPrefs.Save();
                LevelBars[0].interactable = true;
                HeroButtons[0].interactable = true;
                HeroButtons[0].GetComponentInChildren<Text>().text = "SELECT";
                HeroButtons[1].GetComponentInChildren<Text>().text = "UNLOCK LVL 2";
                HeroButtons[2].GetComponentInChildren<Text>().text = "UNLOCK LVL 3";
                HeroButtons[3].GetComponentInChildren<Text>().text = "UNLOCK LVL 4";
            }
            else
            {
                string level = PlayerPrefs.GetString("level");
                string[] levels = level.Split(',');

                if (levels[0] == "1")
                {
                    LevelBars[0].interactable = true;
                    HeroButtons[0].interactable = true;
                    HeroButtons[0].GetComponentInChildren<Text>().text = "SELECT";
                }

                if (levels[1] == "1")
                {
                    LevelBars[1].interactable = true;
                    HeroButtons[1].interactable = true;
                    HeroButtons[1].GetComponentInChildren<Text>().text = "SELECT";
                }
                else
                {
                    HeroButtons[1].GetComponentInChildren<Text>().text = "UNLOCK LVL 2";
                }

                if (levels[2] == "1")
                {
                    LevelBars[2].interactable = true;
                    HeroButtons[2].interactable = true;
                    HeroButtons[2].GetComponentInChildren<Text>().text = "SELECT";
                }
                else
                {
                    HeroButtons[2].GetComponentInChildren<Text>().text = "UNLOCK LVL 3";
                }

                if (levels[3] == "1")
                {
                    LevelBars[3].interactable = true;
                    HeroButtons[3].interactable = true;
                    HeroButtons[3].GetComponentInChildren<Text>().text = "SELECT";
                }
                else
                {
                    HeroButtons[3].GetComponentInChildren<Text>().text = "UNLOCK LVL 4";
                }

                if (levels[4] == "1")
                {
                    LevelBars[4].interactable = true;
                }
            }
        }
        catch (Exception err)
        {
            // leave blank
        }
    }

    public void SelectedHero()
    {
        try
        {
            int get_select = PlayerPrefs.GetInt("SELECTED_HERO", 1);
            if (get_select == 1)
            {
                PlayerPrefs.SetInt("SELECTED_HERO", 1);
                HeroButtons[0].GetComponentInChildren<Text>().text = "SELECTED";
            }
            else if (get_select == 2)
            {
                PlayerPrefs.SetInt("SELECTED_HERO", 2);
                HeroButtons[1].GetComponentInChildren<Text>().text = "SELECTED";
            }
            else if (get_select == 3)
            {
                PlayerPrefs.SetInt("SELECTED_HERO", 3);
                HeroButtons[2].GetComponentInChildren<Text>().text = "SELECTED";
            }
            else if (get_select == 4)
            {
                PlayerPrefs.SetInt("SELECTED_HERO", 4);
                HeroButtons[3].GetComponentInChildren<Text>().text = "SELECTED";
            }

            PlayerPrefs.Save();
        }
        catch (Exception err)
        {
            // leave blank
        }
    }

    public void SelectHero_01()
    {
        PlayerPrefs.SetInt("SELECTED_HERO", 1);
        PlayerPrefs.Save();

        UpdateLevel();
        SelectedHero();
    }

    public void SelectHero_02()
    {
        PlayerPrefs.SetInt("SELECTED_HERO", 2);
        PlayerPrefs.Save();

        UpdateLevel();
        SelectedHero();
    }

    public void SelectHero_03()
    {
        PlayerPrefs.SetInt("SELECTED_HERO", 3);
        PlayerPrefs.Save();

        UpdateLevel();
        SelectedHero();
    }

    public void SelectHero_04()
    {
        PlayerPrefs.SetInt("SELECTED_HERO", 4);
        PlayerPrefs.Save();

        UpdateLevel();
        SelectedHero();
    }
}

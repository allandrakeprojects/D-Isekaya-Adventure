using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public GameObject[] LevelBars;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is Null!");
            }
            return _instance;
        }
    }

    private void Start()
    {
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        try
        {
            int level = PlayerPrefs.GetInt("level_selected");
            if (level == 2)
            {
                LevelBars[0].SetActive(true);
            }

            if (level == 3)
            {
                LevelBars[1].SetActive(true);
            }

            if (level == 4)
            {
                LevelBars[2].SetActive(true);
            }

            if (level == 5)
            {
                LevelBars[3].SetActive(true);
            }
        }
        catch (Exception err)
        {
            // leave blank
        }
    }

    //public Text playerCoinCountText;
    //public Image selectionImage;
    public Text coinCountText;
    public Image[] healthBars;

    private void Awake()
    {
        _instance = this;
    }

    public void OpenShop(int gemCount)
    {
        //playerCoinCountText.text = "" + gemCount + "G";
    }

    public void UpdateShopSelection(int yPos)
    {
        //selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateCoinCount(int count)
    {
        coinCountText.text = "" + count;
        PlayerPrefs.SetInt("score", count);

        if (PlayerPrefs.GetInt("highscore", 0) < count)
        {
            PlayerPrefs.SetInt("highscore", count);
            PlayerPrefs.SetInt("score", 10000);
        }
        else
        {
            PlayerPrefs.SetInt("score", count);
        }

        PlayerPrefs.Save();
    }
    
    public void RemoveLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }

    public void AddLives(int livesRemaining)
    {
        for (int i = 0; i <= livesRemaining; i++)
        {
            if (i == livesRemaining)
            {
                healthBars[i - 1].enabled = true;
            }
        }

    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
}


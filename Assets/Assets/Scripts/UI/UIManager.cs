using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

        //healthBars[0].enabled = false;

    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    public Text heartCountText;
    public Text messageHeartText;
    bool detectStartCountdown = false;

    // Start is called before the first frame update
    void Start()
    {
        detectHeart();
        PlayerPrefs.GetInt("heart", 1);
    }

    public void detectHeart()
    {
        if (PlayerPrefs.GetInt("heart") > 0)
        {
            showHeartTotal();
        }
        else
        {
            showHeartCountdown();
        }
    }

    public void showHeartTotal()
    {
        PlayerPrefs.SetInt("heart", PlayerPrefs.GetInt("heart"));
        PlayerPrefs.Save();
        heartCountText.text = PlayerPrefs.GetInt("heart").ToString();
    }

    public void showHeartCountdown()
    {
        detectStartCountdown = true;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (detectStartCountdown)
            {
                DateTime datevalue1 = DateTime.Parse(PlayerPrefs.GetString("waitDate"));
                DateTime datevalue2 = DateTime.Now;
                TimeSpan timeDifference = datevalue1 - datevalue2;
                string time = new DateTime(timeDifference.Ticks).ToString("mm:ss");
                heartCountText.text = time;

                if (int.Parse(time.Replace(":", "")) <= 0000)
                {
                    PlayerPrefs.SetInt("heart", 1);
                    PlayerPrefs.Save();
                    heartCountText.text = PlayerPrefs.GetInt("heart").ToString();
                    detectStartCountdown = false;
                    messageHeartText.text = "";
                }
            }
        }
        catch (Exception err)
        {
            PlayerPrefs.SetInt("heart", 1);
            PlayerPrefs.Save();
            heartCountText.text = PlayerPrefs.GetInt("heart").ToString();
            detectStartCountdown = false;
            messageHeartText.text = "";
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private Music music;
    public Button musicToggleButton;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindObjectOfType<Music>();
        UpdateIconAndVolume();
    }

    public void PauseMusic()
    {
        music.ToggleSound();
        UpdateIconAndVolume();
    }

    public void UpdateIconAndVolume()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            musicToggleButton.GetComponent<Image>().sprite = musicOnSprite;
            AudioListener.pause = false;
        }
        else
        {
            musicToggleButton.GetComponent<Image>().sprite = musicOffSprite;
            AudioListener.pause = true;
        }
    }
}

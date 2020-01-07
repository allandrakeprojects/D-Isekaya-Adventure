using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedCollider : MonoBehaviour
{
    public GameObject previousLevel;
    public GameObject previousPlayer;
    public GameObject previousCam;
    public GameObject nextLevel;
    public GameObject nextPlayer;
    public GameObject nextCam;
    [SerializeField]
    public int level;
    [SerializeField]
    public float x;
    [SerializeField]
    public float y;
    [SerializeField]
    public float z;

    private LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            UpdateLevel();

            previousLevel.SetActive(false);
            previousPlayer.SetActive(false);
            previousCam.SetActive(false);

            nextLevel.SetActive(true);
            nextPlayer.SetActive(true);
            nextCam.SetActive(true);

            player.transform.position = new Vector3(x, y, z);
        }
    }

    private void UpdateLevel()
    {
        string get_level = PlayerPrefs.GetString("level");
        Debug.Log(level);
        string[] levels = get_level.Split(',');

        if (level == 2)
        {
            string final_level_update = levels[0] + ',' + '1' + ',' + levels[2] + ',' + levels[3] + ',' + levels[4];
            PlayerPrefs.SetString("level", final_level_update);
            PlayerPrefs.Save();
        }
        else if (level == 3)
        {
            string final_level_update = levels[0] + ',' + levels[1] + ',' + '1' + ',' + levels[3] + ',' + levels[4];
            PlayerPrefs.SetString("level", final_level_update);
            PlayerPrefs.Save();
        }
        else if (level == 4)
        {
            string final_level_update = levels[0] + ',' + levels[1] + ',' + levels[2] + ',' + '1' + ',' + levels[4];
            PlayerPrefs.SetString("level", final_level_update);
            PlayerPrefs.Save();
        }
        else if (level == 5)
        {
            string final_level_update = levels[0] + ',' + levels[1] + ',' + levels[2] + ',' + levels[3] + ',' + '1';
            PlayerPrefs.SetString("level", final_level_update);
            PlayerPrefs.Save();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelected : MonoBehaviour
{
    public GameObject previousLevelOne;
    public GameObject previousPlayerOne;
    public GameObject previousCamOne;

    public GameObject previousLevel;
    public GameObject previousPlayer;
    public GameObject previousCam;

    public GameObject nextLevel;
    public GameObject nextPlayer;
    public GameObject nextCam;
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
            previousLevelOne.SetActive(false);
            previousPlayerOne.SetActive(false);
            previousLevel.SetActive(false);
            //previousPlayer.SetActive(false);
            //previousCam.SetActive(false);

            nextLevel.SetActive(true);
            //nextPlayer.SetActive(true);
            //nextCam.SetActive(true);


            int get_level = PlayerPrefs.GetInt("level_selected");

            if (get_level == 1)
            {
                player.transform.position = new Vector3(-33.6f, -94.415f, z);
            }
            else if (get_level == 2)
            {
                player.transform.position = new Vector3(60.2f, -125.3f, z);
            }
            else if (get_level == 3)
            {
                player.transform.position = new Vector3(143.1f, -167.5f, z);
            }
            else if (get_level == 4)
            {
                player.transform.position = new Vector3(190.54f, -206.8f, z);
            }
            else if (get_level == 5)
            {
                player.transform.position = new Vector3(248.88f, -243.8f, z);
            }
        }
    }
}
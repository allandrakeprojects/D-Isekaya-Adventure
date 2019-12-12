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
            previousLevel.SetActive(false);
            previousPlayer.SetActive(false);
            previousCam.SetActive(false);

            nextLevel.SetActive(true);
            nextPlayer.SetActive(true);
            nextCam.SetActive(true);

            player.transform.position = new Vector3(x, y, z);
        }
    }
}

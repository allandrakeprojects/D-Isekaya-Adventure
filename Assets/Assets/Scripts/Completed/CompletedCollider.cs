using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedCollider : MonoBehaviour
{
    public GameObject previousLevel;
    public GameObject nextLevel;
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
            nextLevel.SetActive(true);
            player.transform.position = new Vector3(x, y, z);
        }
    }
}

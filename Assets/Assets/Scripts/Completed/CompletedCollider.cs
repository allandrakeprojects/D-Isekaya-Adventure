using UnityEngine;

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
            //previousPlayer.SetActive(false);
            //previousCam.SetActive(false);

            nextLevel.SetActive(true);
            //nextPlayer.SetActive(true);
            //nextCam.SetActive(true);

            int get_level = PlayerPrefs.GetInt("level_selected");
            if (x != 0)
            {
                get_level += 1;
                PlayerPrefs.SetInt("level_selected", get_level);
                PlayerPrefs.Save();
            }

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

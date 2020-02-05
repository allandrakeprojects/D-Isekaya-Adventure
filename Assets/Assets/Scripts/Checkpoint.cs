using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            PlayerPrefs.SetFloat("LAST_PLAYER_X", player.transform.localPosition.x);
            PlayerPrefs.SetFloat("LAST_PLAYER_Y", player.transform.localPosition.y);
            PlayerPrefs.SetFloat("LAST_PLAYER_Z", player.transform.localPosition.z);
            PlayerPrefs.Save();
        }
    }
}

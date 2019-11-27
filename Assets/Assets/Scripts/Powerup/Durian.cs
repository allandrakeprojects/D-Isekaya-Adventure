using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Durian : MonoBehaviour
{
    public int speed = 4;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.AddSpeed(speed);
                Destroy(this.gameObject);
            }
        }
    }
}

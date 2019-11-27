﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mango : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.EnableFlame();
                Destroy(this.gameObject);
            }
        }
    }
}

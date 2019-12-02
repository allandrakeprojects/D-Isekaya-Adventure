using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
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
			levelManager.LoadWinMenuAfterDelay();
		}
	}
}

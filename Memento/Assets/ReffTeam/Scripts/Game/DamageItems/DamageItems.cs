using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItems : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerPlatformerController l_Player = collision.gameObject.GetComponent<PlayerPlatformerController>();
            if (l_Player != null)
            {
                l_Player.Kill();
            }
            else
            {
                Debug.Log("WARNING: Missing PlayerPlatformerController Script on player!!");
            }

        }
    }
}

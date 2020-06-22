using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                player.Die();
            }
        }
    }
}

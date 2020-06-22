using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDoubleJump : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            var pc = col.GetComponent<PlayerController>();
            if(pc != null && pc.SetDoubleJump(2))
            {
                // show other fanfare here
            }
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractable : MonoBehaviour
{

    [SerializeField]
    private GameObject chatBox;

    private void OnTriggerEnter2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            chatBox.SetActive(true);
        }
    }

    private void OnTriggerExit2D (Collider2D col)
    {
        if(col.tag == "Player")
        {
            chatBox.SetActive(false);
        }
    }
}

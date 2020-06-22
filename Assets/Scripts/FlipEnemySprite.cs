using UnityEngine;

public class FlipEnemySprite : MonoBehaviour
{

    private SpriteRenderer sp;
    [SerializeField]
    private bool flipped = false;

    private Vector3 oldPosition = Vector2.zero;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var diff = transform.position - oldPosition;
        if(diff.x > 0)
        {
            sp.flipX = !flipped;
        }
        else
        {
            sp.flipX = flipped;
        }
        oldPosition = transform.position;
    }
}

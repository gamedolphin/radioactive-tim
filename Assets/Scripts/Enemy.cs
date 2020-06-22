using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{

    private SpriteRenderer sp;
    private Collider2D col;
    private AudioSource aud;

    [SerializeField]
    private ParticleSystem pc;

    [SerializeField]
    private AudioClip deathClip;

    private bool dead = false;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        aud = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var p = collision.gameObject.GetComponent<PlayerController>();
            var point = collision.contacts[0];
            if (p != null)
            {
                if (point.normal.x <= 0.1f)
                {
                    // kill self
                    aud.PlayOneShot(deathClip);
                    p.ForceJump();
                    Die();
                }
                else
                {


                    p.Die();
                }
            }
        }
    }

    private void Die()
    {
        if(dead) {
            return;
        }
        sp.enabled = false;
        col.enabled = false;
        pc.Play();
        dead = true;
        ScoreKeeper.Score += 1;
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

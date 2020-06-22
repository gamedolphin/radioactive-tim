using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Key : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem pc;

    [SerializeField]
    private AudioClip deathClip;

    [SerializeField]
    private GameObject door;

    private SpriteRenderer sp;
    private Collider2D col;
    private AudioSource aud;

    private bool dead = false;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Die();
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
        Destroy(door);
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

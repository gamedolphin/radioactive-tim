using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Goop : MonoBehaviour
{
    private SpriteRenderer sp;
    private Collider2D c;
    private AudioSource aud;


    [SerializeField]
    private ParticleSystem pc;
    [SerializeField]
    private AudioClip deathSound;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            c.enabled = false;
            sp.enabled = false;
            pc.Play();
            aud.PlayOneShot(deathSound);
            ScoreKeeper.Score += 1;
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}

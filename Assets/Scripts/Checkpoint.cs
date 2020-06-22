using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Checkpoint : MonoBehaviour
{
    private bool active = false;
    private AudioSource aud;

    [SerializeField]
    private ParticleSystem pc;

    [SerializeField]
    private AudioClip checkpointClip;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if(col.tag == "Player")
        {
            var p = col.GetComponent<PlayerController>();
            p.SetLastCheckpoint(transform.position);

            Activate();
        }
    }

    private void Activate()
    {
        if(!active)
        {
            active = true;
            pc.Play();
            aud.PlayOneShot(checkpointClip);
        }
    }
}

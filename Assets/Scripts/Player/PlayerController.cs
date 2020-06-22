using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(PhysicsObject))]
public class PlayerController : MonoBehaviour
{
    private PhysicsObject pb;
    private SpriteRenderer sr;
    private AudioSource aud;
    private Animator animator;
    private float lastJumpTime;
    private int jumpCount = 0;

    [HideInInspector]
    public Vector2 ExternalVelocity = Vector2.zero;

    [SerializeField]
    private float jumpTakeOffSpeed = 7;
    [SerializeField]
    private float maxSpeed = 7;
    [SerializeField]
    private float pressJumpTime = 0.25f;
    [SerializeField]
    private int maxJumpCount = 1;

    [SerializeField]
    private ParticleSystem dust;
    [SerializeField]
    private ParticleSystem death;

    [SerializeField]
    private AudioClip jumpAudio;

    [SerializeField]
    private AudioClip deathAudio;

    [SerializeField]
    private AudioClip respawnAudio;

    private Vector2 lastCheckpoint = Vector2.zero;
    private bool dead = false;

    private void OnEnable()
    {
        pb = GetComponent<PhysicsObject>();
        sr = GetComponent<SpriteRenderer>();
        aud = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        lastCheckpoint = transform.position;
    }

    public void SetLastCheckpoint(Vector2 point)
    {
        lastCheckpoint = point;
    }

    public void Die()
    {
        if(dead) return;

        dead = true;
        sr.enabled = false;
        death.Play();
        aud.PlayOneShot(deathAudio);
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(1);
        transform.position = lastCheckpoint;
        sr.enabled = true;
        aud.PlayOneShot(respawnAudio);
        yield return new WaitForSeconds(1);
        dead = false;
    }

    public bool SetDoubleJump(int count)
    {
        if (count == maxJumpCount) {
            return false;
        }
        maxJumpCount = count;
        return true;
    }

    private void Update()
    {
        pb.TargetVelocity = Vector3.zero;
        if(dead) return;

        lastJumpTime -= Time.deltaTime;

        if(pb.Grounded)
        {
            jumpCount = 0;
        }

        ComputeVelocity();
    }

    public void ForceJump()
    {
        pb.Velocity = new Vector2(pb.Velocity.x, jumpTakeOffSpeed);
    }

    private void ComputeVelocity()
    {
        var move = Vector2.zero;
        move.x = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            lastJumpTime = pressJumpTime;
        }

        if(lastJumpTime > 0 && (pb.Grounded || jumpCount < maxJumpCount - 1))
        {
            lastJumpTime = 0;
            jumpCount += 1;
            pb.Velocity = new Vector2(pb.Velocity.x, jumpTakeOffSpeed);
            dust.Play();
            aud.PlayOneShot(jumpAudio);
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if(pb.Velocity.y  > 0)
            {
                pb.Velocity = new Vector2(pb.Velocity.x, pb.Velocity.y * 0.5f);
            }
        }

        bool flipSprite = sr.flipX ? (move.x > 0.01f) : (move.x < -0.01f);
        if(flipSprite) {
            sr.flipX = !sr.flipX;
            dust.Play();
        }

        var actualSpeed = pb.Velocity - ExternalVelocity;

        animator.SetBool("grounded", pb.Grounded);
        animator.SetFloat("velocityX", Mathf.Abs(actualSpeed.x/maxSpeed));
        animator.SetFloat("velocityY", pb.Velocity.y);

        pb.TargetVelocity = move * maxSpeed + ExternalVelocity;
    }
}

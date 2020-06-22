using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [SerializeField]
    private float gravityModifier = 1f;
    [SerializeField]
    private float minGroundNormalY = 0.65f;

    private Vector2 targetVelocity = Vector2.zero;
    private bool grounded = false;
    private Vector2 velocity = Vector2.zero;
    private Vector2 groundNormal = Vector2.up;

    public bool Grounded => grounded;
    public Vector2 Velocity
    {
        get { return rb2d.velocity; }
        set { rb2d.velocity = value; }
    }

    public float Gravity
    {
        get { return gravityModifier; }
        set { gravityModifier = value; }
    }

    public Vector2 TargetVelocity
    {
        set { targetVelocity = value; }
    }

    private const float minMoveDistance = 0.001f;
    private const float shellRadius = 0.01f;

    private Rigidbody2D rb2d;
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>();

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        SetXVelocity();
        // velocity += gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        // velocity.x = targetVelocity.x;

        // grounded = false;

        // var deltaPosition = velocity * Time.fixedDeltaTime;
        // var moveAlongGround = new Vector2(groundNormal.y, - groundNormal.x);

        // var move = moveAlongGround * deltaPosition.x;

        // Movement(move, false);

        // move = Vector2.up * deltaPosition.y;

        // Movement(move, true);
    }

    private void SetXVelocity()
    {
        rb2d.velocity = new Vector2(targetVelocity.x, rb2d.velocity.y);
    }

    private void CheckGrounded()
    {
        grounded = false;
        var moveY = rb2d.velocity.y * Time.fixedDeltaTime;
        var distanceY = moveY < -0.1f ? moveY : -0.1f;
        var magnitude = Mathf.Abs(distanceY);
        int count = rb2d.Cast(new Vector2(0,distanceY), contactFilter, hitBuffer, magnitude + shellRadius);
        hitBufferList.Clear();
        for (int i = 0; i < count; ++i) {
            hitBufferList.Add(hitBuffer[i]);
        }

        for (int i = 0; i < hitBufferList.Count; ++i) {
            var currentNormal = hitBufferList[i].normal;
            if (currentNormal.y > minGroundNormalY) {
                grounded = true;
                break;
            }

        }
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        // var distance = move.magnitude;
        // if(distance > minMoveDistance)
        // {
        //     int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
        //     hitBufferList.Clear();
        //     for (int i = 0; i < count; ++i) {
        //         hitBufferList.Add(hitBuffer[i]);
        //     }

        //     for (int i = 0; i < hitBufferList.Count; ++i) {
        //         var currentNormal = hitBufferList[i].normal;
        //         if (currentNormal.y > minGroundNormalY) {
        //             grounded = true;
        //             if (yMovement) {
        //                 groundNormal = currentNormal;
        //                 currentNormal.x = 0;
        //             }
        //         }

        //         var projection = Vector2.Dot(velocity, currentNormal);

        //         if (projection < 0) {
        //             velocity = velocity - projection * currentNormal;
        //         }

        //         float modifiedDistance = hitBufferList[i].distance - shellRadius;
        //         distance = modifiedDistance < distance ? modifiedDistance : distance;
        //     }

        // }
        // rb2d.MovePosition(move);
    }
}

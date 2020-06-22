using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform waypointContainer;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float delay = 3;

    private Transform[] childTransforms;
    private int currentWaypoint = 0;

    private float delay_start;

    private PlayerController player = null;

    private void Awake()
    {
        childTransforms = waypointContainer.Cast<Transform>().ToArray();
    }

    private void Update()
    {
        MoveToWaypoint();
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(childTransforms.Length < 1) {
            return;
        }
        var currentTarget = childTransforms[currentWaypoint];
        if(player != null)
        {
            var direction = currentTarget.position - transform.position;
            if(direction.magnitude > 0.1f)
            {
                player.ExternalVelocity = speed * direction.normalized;
            }
            else {
                player.ExternalVelocity = Vector2.zero;
            }
        }
    }

    private void MoveToWaypoint()
    {
        if(childTransforms.Length < 1) {
            return;
        }
        var currentTarget = childTransforms[currentWaypoint];

        if(Vector3.Distance(transform.position,currentTarget.position) < 0.1f)
        {
            UpdateTarget();
        }
        else
        {
            MoveToTarget(currentTarget.position);
        }
    }

    private void UpdateTarget()
    {
        if(Time.time - delay_start > delay)
        {
            currentWaypoint += 1;
            if(currentWaypoint >= childTransforms.Length) {
                currentWaypoint = 0;
            }
            delay_start = Time.time;
        }
    }

    private void MoveToTarget(Vector3 position)
    {
        transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player") {
            var dir = (col.gameObject.transform.position - transform.position).normalized;
            if(dir.y > 0)
            {
                player = col.gameObject.GetComponent<PlayerController>();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player") {
            if(player != null)
            {
                player.ExternalVelocity = Vector2.zero;
            }
            player = null;
        }
    }
}

using UnityEngine;
using System.Linq;

public class Patrol : MonoBehaviour
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

    private void Awake()
    {
        childTransforms = waypointContainer.Cast<Transform>().ToArray();
    }

    private void Update()
    {
        MoveToWaypoint();
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
}

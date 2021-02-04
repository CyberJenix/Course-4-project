
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    public bool isPathClosed;
    public float speed, angularSpeed, delay;
    public Transform pathHolder;
    public int trustfulness;
    public float threshold;

    private int curWaypointIndex, newWayPointIndex;
    private Vector3 curWaypoint, distanceToWaypoint;
    private Vector3[] waypoints;
    private NavMeshAgent agent;
    private bool isInAudibleRange, isInSightRange, isInAttackRange, busy, walkForward;

    enum State
    { 
        Calm,
        Alert,
        Alarmed,
    }

    private State curState;

    private void OnDrawGizmos()
    {
        Vector3 startPos = pathHolder.GetChild(0).position;
        Vector3 prevPos = startPos;
        foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevPos, waypoint.position);
            prevPos = waypoint.position;
        }
        if(isPathClosed) Gizmos.DrawLine(prevPos, startPos);
    }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;
        curState = State.Calm;
    }

    // Start is called before the first frame update
    void Start()
    {
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++) 
        {
            waypoints[i] = pathHolder.GetChild(i).position;
        }
        curWaypointIndex = 0;
        curWaypoint = waypoints[curWaypointIndex];
        walkForward = true;

    }

    void Patroling() 
    {
        if (!busy)
        {
            agent.SetDestination(curWaypoint);
            busy = !busy;
        }

        distanceToWaypoint = transform.position - curWaypoint;

        if (distanceToWaypoint.magnitude <= threshold) 
        {
            newWayPointIndex = curWaypointIndex + 1;
            if (newWayPointIndex > 0 && newWayPointIndex % waypoints.Length == 0 && !isPathClosed)
                walkForward = false;
            else if (newWayPointIndex <= 1 && !isPathClosed)
                walkForward = true;

            if (!walkForward)
            {
                newWayPointIndex = curWaypointIndex - 1;
            }

            curWaypointIndex = newWayPointIndex % waypoints.Length;
            curWaypoint = waypoints[curWaypointIndex];
            Invoke(nameof(LookAround), delay);
        }
    }

    void LookAround() { busy = false; }

    void Investigating()
    {
        
    }

    void Attack()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (curState) 
        {
            case State.Calm:
                Patroling();
                break;
            case State.Alert:
                Investigating();
                break;
            case State.Alarmed:
                Attack();
                break;
            default:
                break;
        }
    }
}

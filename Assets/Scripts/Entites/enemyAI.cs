using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    // Generic variables
    private NavMeshAgent agent;
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;

    public Transform activeEnemy;
    private LayerMask isDefault;
    public LayerMask isGround, isPlayer;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    // Patroling
    private bool walkPointSet;

    [SerializeField] private Transform pathHolder;
    [SerializeField] private Vector3[] waypoints;
    [SerializeField] private Vector3 curWayPoint;
    private int curWaypointIndex, newWayPointIndex;
    private bool walkForward, busy;
    [SerializeField] private bool isPathClosed;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float delay;

    // Attacking
    [SerializeField]private float timeBetweenAttacks;
    private bool alreadyAttacked, enemyAcquired;

    // Sensors
    [SerializeField] private float DOV;
    [SerializeField] private float FOV;
    [SerializeField] private float attackRange;

    // State switch conditions
    bool playersInSight, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;

        isDefault = LayerMask.GetMask("Default");
        StartCoroutine("FindWithDelay", .5f);
        enemyAcquired = false;
    }

    private void Start()
    {
        if(pathHolder)
        {
            waypoints = new Vector3[pathHolder.childCount];
            for (int i = 0; i < waypoints.Length; i++)
            {
                waypoints[i] = pathHolder.GetChild(i).position;
            }
            curWaypointIndex = 0;
            curWayPoint = waypoints[curWaypointIndex];
            walkForward = true;
        }

    }

    // Update is called once per frame
    private void Update()
    {
        playersInSight = targets.Count > 0;

        if (playersInSight)
            AcquireNearestTarget();

        if (!playersInSight && !playerInAttackRange) Patrolling();
        if ( enemyAcquired  && !playerInAttackRange) ChasePlayer();
        if ( enemyAcquired  &&  playerInAttackRange) AttackPlayer();

    }

    IEnumerator FindWithDelay(float delay) 
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    private void FindVisibleTarget() 
    {
        targets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, DOV, isPlayer);

        foreach (Collider target in targetsInViewRadius) 
        {
            Transform tTransform = target.transform;
            Vector3 dirToTarget = (tTransform.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < FOV * .5)
            {
                float distToTarget = Vector3.Distance(transform.position, tTransform.position);
                Ray ray = new Ray(transform.position, dirToTarget);

                bool behindObstacle = Physics.Raycast(ray, distToTarget, isDefault);
                bool behindGround = Physics.Raycast(ray, distToTarget, isGround);
                if (!behindGround && !behindObstacle)
                    targets.Add(tTransform);
                    
            }
        }
    }

    private Vector3 DirFromAngle(float angle, bool isAngleGlobal) 
    {
        if (!isAngleGlobal)
            angle += transform.eulerAngles.y;

        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    private void Patrolling() 
    {
        if (pathHolder)
        {
            Point2Point();
            return;
        }
        Wandering();
    }

    private void AcquireNearestTarget() 
    {
        activeEnemy = targets[0];
        foreach (Transform target in targets) 
        {
            if (Vector3.Distance(transform.position, target.position) < Vector3.Distance(transform.position, activeEnemy.position))
                activeEnemy = target;
        }
        enemyAcquired = true;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-DOV, DOV);
        float randomX = Random.Range(-DOV, DOV);

        float x = transform.position.x + randomX;
        float z = transform.position.z + randomZ;

        Vector3 temp = new Vector3(x, transform.position.y + 100.0f, z);
        Vector3 down = new Vector3(x, transform.position.y, z);
        
        Ray ray = new Ray(temp, down);
        RaycastHit hit;

        Physics.Raycast(ray, out hit, isGround);

        float y = transform.position.y + hit.distance;

        curWayPoint = new Vector3(x, y, z);

        if (Physics.Raycast(curWayPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }

    private void Point2Point() 
    {
        if (!busy)
        {
            agent.SetDestination(curWayPoint);
            busy = true;
        }

        Vector3 distanceToWaypoint = transform.position - curWayPoint;

        if (distanceToWaypoint.magnitude <= distanceThreshold)
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
            curWayPoint = waypoints[curWaypointIndex];
            Invoke(nameof(LookAround), delay);
        }
    }

    private void LookAround() { busy = false; }

    private void Wandering()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet) 
            agent.SetDestination(curWayPoint);

        Vector3 distanceToWalkPoint = transform.position - curWayPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < distanceThreshold)
            walkPointSet = false;
    }

    private void ChasePlayer()
    {
        
          agent.SetDestination(activeEnemy.position);
          
          Vector3 distToTarg = transform.position - activeEnemy.position;
          if (distToTarg.magnitude < attackRange - .1f)
            playerInAttackRange = true;
         
    }

    

    private void AttackPlayer()
    {
        Vector3 distToTarg = transform.position - activeEnemy.position;
        if (distToTarg.magnitude > attackRange)
            playerInAttackRange = false;

        if (!alreadyAttacked)
        {
            ///Attack code
            
            
            ///

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = !alreadyAttacked;
    }

    private void OnDrawGizmos()
    {
        // Attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Distance of view
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DOV);

        // Field of view
        Vector3 tempA = DirFromAngle(FOV * .5f, false);
        Vector3 tempB = DirFromAngle(-FOV * .5f, false);
        Gizmos.DrawRay(transform.position, tempA * DOV);
        Gizmos.DrawRay(transform.position, tempB * DOV);

        // All visible targets
        foreach (Transform target in targets)
            Gizmos.DrawLine(transform.position, target.position);

        // Active target
        Gizmos.color = Color.red;
        if (enemyAcquired)
            Gizmos.DrawLine(transform.position, activeEnemy.position);

        //Path waypoints
        if(pathHolder)
        {
            Vector3 startPos = pathHolder.GetChild(0).position;
            Vector3 prevPos = startPos;
            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, .3f);
                Gizmos.DrawLine(prevPos, waypoint.position);
                prevPos = waypoint.position;
            }
            if (isPathClosed) Gizmos.DrawLine(prevPos, startPos);
            return;
        }
        // Random walk points
        Gizmos.DrawSphere(curWayPoint, .5f);
    }

}

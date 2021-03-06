﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    // Generic variables
    private Rigidbody body;
    private EnemyStats stats;
    private NavMeshAgent agent;
    [SerializeField] private float speed;
    [SerializeField] private float angularSpeed;
    
    
    private LayerMask isDefault;
    [SerializeField] private LayerMask isGround, isPlayer;

     private Transform activeEnemy;
    [SerializeField] private List<Transform> targets = new List<Transform>();

    // Patroling
    private bool walkPointSet;
    [SerializeField] private Transform pathHolder;
    private Vector3[] waypoints;
    private Vector3 curWayPoint;
    private int curWaypointIndex, newWayPointIndex;
    private bool walkForward, busy;
    [SerializeField] private bool isPathClosed;
    [SerializeField] private float distanceThreshold;
    [SerializeField] private float delay;

    // Attacking
    private bool alreadyAttacked, enemyAcquired;
    private Transform attackPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun, hand;
    [SerializeField] private float timeBetweenAttacks, spread, shootForce;
    [SerializeField] private float attackRange;


    // Sensors
    [SerializeField] private float DOV;
    [SerializeField] private float FOV;

    // State switch conditions
    private bool playersInSight, playerInAttackRange;

    private void Awake()
    {
        hand        = gameObject.transform.Find("Hand");
        gun         = gameObject.transform.Find("Hand/Gun");
        attackPoint = gameObject.transform.Find("Hand/Gun/Barrel");

        body        = GetComponent<Rigidbody>();
        stats       = GetComponent<EnemyStats>();
        agent       = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.angularSpeed = angularSpeed;

        isDefault = LayerMask.GetMask("Default");
        StartCoroutine("FindWithDelay", 1f);
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
        if(!stats.isDead)
        {
            playersInSight = targets.Count > 0;

            if (playersInSight) AcquireNearestTarget();

            if (!playersInSight && !playerInAttackRange) Patrolling();
            if (enemyAcquired && !playerInAttackRange) ChasePlayer();
            if (enemyAcquired && playerInAttackRange) AttackPlayer();
            return;
        }
        agent.isStopped     = true;
        body.isKinematic    = true;
        body.useGravity     = true;
        Destroy(gameObject, 10.0f);


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
            busy = false;
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

        Physics.Raycast(ray, out RaycastHit hit, isGround);

        float y = transform.position.y - hit.distance;

        curWayPoint = new Vector3(x, y, z);

        //if (Physics.Raycast(curWayPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }

    private void Point2Point() 
    {
        if (!busy)
        {
            agent.isStopped = false;
            agent.SetDestination(curWayPoint);
            busy = true;
        }

        Vector3 distanceToWaypoint = transform.position - curWayPoint;

        if (distanceToWaypoint.magnitude <= distanceThreshold)
        {
            newWayPointIndex = curWaypointIndex + 1;
            if (newWayPointIndex > 0 && newWayPointIndex % waypoints.Length == 0 && !isPathClosed) walkForward = false;
            else if (newWayPointIndex <= 1 && !isPathClosed) walkForward = true;

            if (!walkForward) newWayPointIndex = curWaypointIndex - 1;

            curWaypointIndex = newWayPointIndex % waypoints.Length;
            curWayPoint = waypoints[curWaypointIndex];
            Invoke(nameof(LookAround), delay);
        }
    }

    private void LookAround() { busy = false; }

    private void Wandering()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet) agent.SetDestination(curWayPoint);

        Vector3 distanceToWalkPoint = transform.position - curWayPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < distanceThreshold)
            walkPointSet = false;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(activeEnemy.position);
        Vector3 distToTarg = transform.position - activeEnemy.position;

        if (distToTarg.magnitude < attackRange - .5f) 
        { 
            playerInAttackRange = true; 
            agent.isStopped = true; 
        }

        if (distToTarg.magnitude > DOV)
        {
            agent.isStopped = true;
            activeEnemy = null;
            enemyAcquired = false;
        }
    }

    private void AttackPlayer()
    {
        Vector3 distToTarg = transform.position - activeEnemy.position;
        if (distToTarg.magnitude > attackRange) playerInAttackRange = false;

        Vector3 diff = activeEnemy.position - transform.position;
        float angleH = Mathf.Atan2(diff.x, diff.z);
        hand.forward = new Vector3(Mathf.Sin(angleH), 0, Mathf.Cos(angleH));

        if (!alreadyAttacked)
        {
            // Attack code
             
            Shoot();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void Shoot()
    {
        Vector3 directionWithoutSpread = activeEnemy.position - attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //Instantiate projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        //Rotate projectile to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
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

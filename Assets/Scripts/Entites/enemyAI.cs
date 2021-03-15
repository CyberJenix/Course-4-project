using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private LayerMask isDefault;
    public List<Transform> targets = new List<Transform>();
    public Transform activeEnemy;
    public LayerMask isGround, isPlayer;
    

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked, enemyAcquired;

    [SerializeField] private float DOV;
    [SerializeField] private float FOV;
    [SerializeField] private float attackRange;

    bool playersInSight, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        isDefault = LayerMask.GetMask("Default");
        StartCoroutine("FindWithDelay", .5f);
        enemyAcquired = false;
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
            //RaycastHit hit;
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

     // Update is called once per frame
    void Update()
    {
        playersInSight = targets.Count > 0;
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (playersInSight)
            AcquireNearestTarget();
        //if (!playersInSight && !playerInAttackRange) Patroling();
        if ( enemyAcquired && !playerInAttackRange) ChasePlayer();
        if ( enemyAcquired &&  playerInAttackRange) AttackPlayer();
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

        walkPoint = new Vector3(x, y, z);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
            walkPointSet = true;
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if(walkPointSet) 
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1.0f)
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, DOV);

        Vector3 tempA = DirFromAngle(FOV * .5f, false);
        Vector3 tempB = DirFromAngle(-FOV * .5f, false);

        Gizmos.DrawRay(transform.position, tempA * DOV);
        Gizmos.DrawRay(transform.position, tempB * DOV);

        foreach (Transform target in targets)
            Gizmos.DrawLine(transform.position, target.position);

        Gizmos.color = Color.red;
        if (enemyAcquired)
            Gizmos.DrawLine(transform.position, activeEnemy.position);

        Gizmos.DrawSphere(walkPoint, .5f);
    }

}

using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Audio;

// This script is made with the help of Github copilot.
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int damage = 10;
    public float wanderRadius = 5f;
    public float wanderTimer = 5f;
    public float minWanderDistance = 2f;
    public float attackCooldown = 1.5f;
    public float wanderSpeed = 2f;
    public float chaseSpeed = 5f;
    public AudioClip screamSound;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private float timer;
    private bool isScreaming = false;
    private bool isChasing = false;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        timer = wanderTimer;
        lastAttackTime = -attackCooldown;
        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange || isChasing)
        {
            if (!isScreaming && !isChasing)
            {
                StartCoroutine(ScreamAndChase());
            }
            else if (isChasing)
            {
                ChasePlayer();
            }
        }
      
    }
    
    private void AttackPlayer()
    {
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            animator.SetTrigger("attack");
            // Damage the player (assuming the player has a method to take damage)
            //player.GetComponent<PlayerHealth>().TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
    // Chase the player
    private void ChasePlayer()
    {
        isChasing = true;
        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
    }
    // Wander around randomly
    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (!isChasing)
            {
                agent.speed = wanderSpeed;
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, minWanderDistance, -1);
                agent.SetDestination(newPos);
                animator.SetBool("isWalking", true);
                animator.SetBool("isRunning", false);
            }
            yield return new WaitForSeconds(wanderTimer);
        }
    }

    private IEnumerator ScreamAndChase()
    {
        isScreaming = true;
        agent.isStopped = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetTrigger("scream");
        PlayScreamSound();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isScreaming = false;
        isChasing = true;
    }
    // Play the scream sound
    private void PlayScreamSound()
    {
        if (screamSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(screamSound);
        }
    }
    // Get a random position within a sphere
    public static Vector3 RandomNavSphere(Vector3 origin, float maxDist, float minDist, int layermask)
    {
        Vector3 randDirection;
        NavMeshHit navHit;
        do
        {
            randDirection = Random.insideUnitSphere * maxDist;
            randDirection += origin;
        } while (Vector3.Distance(origin, randDirection) < minDist || !NavMesh.SamplePosition(randDirection, out navHit, maxDist, layermask));

        return navHit.position;
    }

    // Draw the detection and attack ranges in the editor
    void OnDrawGizmosSelected()
    {
        // Draw the detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float lookRadius = 500;  // Detection range for player

    Transform target;   // Reference to the player
    Player player;
    NavMeshAgent agent; // Reference to the NavMeshAgent
    public float health = 100;
    //Attacking
    private float timeBetweenAttacks = 1;
    private bool alreadyAttacked;
    private bool Dead = true;
    private float damage = 20;
    private int MoneyPerKill = 100;
    private Animator animator;
    public AudioSource a;
    public AudioClip aDead;
    public AudioClip aAttack;
    public AudioClip Hitmarker;

    // Use this for initialization
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = PlayerManager.instance.player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            animator.SetBool("InRange", false);
            // Move towards the target
            if (Dead)
            {
                agent.SetDestination(target.position);
            }
            FaceTarget();

            // If within attacking distance
            if (distance <= agent.stoppingDistance)
            {
                if (Dead)
                {
                    agent.SetDestination(transform.position);
                }
                FaceTarget();   // Make sure to face towards the target
                AttackPlayer();
            }
        }
    }

    // Rotate to face the target
    void FaceTarget()
    {   
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Show the lookRadius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void AttackPlayer()
    {
        animator.SetBool("InRange", true);

        if (!alreadyAttacked)
        {
            if(player.dead == false)
            {
                a.PlayOneShot(aAttack, 0.2f);
            }
            if(player.dead == true)
            {
                a.enabled = false;
            }
            if (Dead)
            {
                player.TakeDamage(damage);
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(Dead) { a.PlayOneShot(Hitmarker, 5); }

        if (health <= 0 && Dead)
        {
            agent.enabled = false;
            Dead = false;
            a.PlayOneShot(aDead, 1);
            int rand = Random.Range(0, 2);
            if(rand == 0)
            {
                animator.Play("Back");
            }
            else
            {
                animator.Play("Forward");
            }
            player.AddMoney(MoneyPerKill);
            Invoke(nameof(DestroyEnemy), 1);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
        Dead = true;
    }
}

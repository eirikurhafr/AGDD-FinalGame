using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    private Transform target;                                    // target to aim for
    public UserControl[] targetsToKill;
    public GameObject sword;
    public float health = 100;
    private Animator m_Animator;
    public SpawnDamageText damageSpawner;
    private float battleDistance = 1.5f;
    private float attackCooldown = 2.5f;
    public bool dead = false;
    public float attackTimer;
    private bool distanceCheck = false;


    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        agent.stoppingDistance = -1f;
        agent.updateRotation = false;
        agent.updatePosition = true;
        m_Animator = GetComponent<Animator>();
        findClosest();
    }

    public void findClosest()
    {
        float oldDistance = 10f;
        foreach (UserControl player in targetsToKill)
        {
            float newDistance = Vector3.Distance(player.transform.position, transform.position);
            if (newDistance < oldDistance && !player.dead)
            {
                oldDistance = newDistance;
                target = player.transform;
            }
        }
    }

    private void Update()
    {
        if(!dead)
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                if (attackTimer <= 0)
                {
                    if (agent.remainingDistance > agent.stoppingDistance)
                    {
                        if (agent.remainingDistance < battleDistance)
                        {
                            character.Move(agent.desiredVelocity / 3, false, false);
                        }
                        else
                        {
                            character.Move(agent.desiredVelocity, false, false);
                        }
                        if (!distanceCheck)
                        {
                            agent.stoppingDistance = 1f;
                            distanceCheck = true;
                        }
                    }
                    else if (agent.remainingDistance < agent.stoppingDistance)
                    {
                        doAttack();
                    }
                }
                else if (attackTimer > 0)
                {
                    if (attackTimer < 1)
                        findClosest();
                    attackTimer -= Time.deltaTime;
                }
            }
            else if(target == null)
            {
                findClosest();
            }
        }
        else
        {
            m_Animator.SetBool("AttackingGrounded", false);
        }
    }

    private void hurt(float damage)
    {
        health -= damage;
        damageSpawner.spawnText(damage.ToString());
        if (health < 0)
        {
            m_Animator.SetBool("Death", true);
            dead = true;
        }
    }

    public void doAttack()
    {
        character.Move(Vector3.zero, false, false);
        agent.velocity = Vector3.zero;
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
        m_Animator.SetBool("AttackingGrounded", true);
        attackTimer = attackCooldown;
    }

    public void enableCollision()
    {
        sword.SendMessage("turnOnCollider");
    }

    public void disableCollision()
    {
        sword.SendMessage("turnOffCollider");
    }

    public void stopAttack()
    {
        m_Animator.SetBool("AttackingGrounded", false);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

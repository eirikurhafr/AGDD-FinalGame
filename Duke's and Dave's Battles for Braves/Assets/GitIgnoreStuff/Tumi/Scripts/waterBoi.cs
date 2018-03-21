﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBoi : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for
    public UserControl[] targetsToKill;
    public Transform prefab;
    public float health = 100;
    private bool vulnerable = false;
    public bool bla = false;
    private Animator m_Animator;
    public GameObject hand;
    private float battleDistance = 5f;
    private float attackCooldown = 0.6f;
    private float attackAirCooldown = 2.7f;
    private bool dead = false;
    public float attackTimer;


    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        agent.stoppingDistance = 2.5f;
        agent.updateRotation = false;
        agent.updatePosition = true;
        m_Animator = GetComponent<Animator>();
        findClosest();
    }

    public void findClosest()
    {
        float oldDistance = 100f;
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
        if(bla)
        {
            spillWater();
            bla = false;
        }

        if (!dead)
        {
            if (attackTimer <= 0)
            {
                if (target != null)
                {
                    agent.SetDestination(target.position);
                }
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    if (agent.remainingDistance < battleDistance)
                    {
                        agent.speed = 1.5f;
                        character.Move(agent.desiredVelocity/2, false, false);
                    }
                    else
                    {
                        agent.speed = 3f;
                        character.Move(agent.desiredVelocity, false, false);
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
                else if (attackTimer < 0.5)
                    m_Animator.SetBool("Stunned", false);
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            m_Animator.SetBool("Stunned", false);
            m_Animator.SetBool("AttackingGroundpound", false);
            m_Animator.SetBool("AttackingGrounded", false);
        }
    }

    private void spillWater()
    {
        if(!vulnerable)
        {
            vulnerable = true;
            m_Animator.SetBool("Stunned", true);
            attackTimer = 8f;
        }
    }

    private void hurt(float damage)
    {
        if(vulnerable)
        {
            health -= damage;
            if (health < 0)
            {
                m_Animator.SetBool("Death", true);
                dead = true;
            }
        }
    }

    public void doAttack()
    {
        character.Move(Vector3.zero, false, false);
        agent.velocity = Vector3.zero;
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
        int number = Random.Range(0, 4);
        if(number == 0)
        {
            m_Animator.SetBool("AttackingGroundpound", true);
            attackTimer = attackAirCooldown;
        }
        else
        {
            m_Animator.SetBool("AttackingGrounded", true);
            attackTimer = attackCooldown;
        }
        
    }

    public void groundPoundAttack()
    {
        Vector3 temp;
        temp.x = transform.position.x;
        temp.z = transform.position.z + 3.8f;
        temp.y = transform.position.y;
        Instantiate(prefab, temp, Quaternion.identity);
    }

    public void enableCollision()
    {
        hand.SendMessage("turnOnCollider");
    }

    public void disableCollision()
    {
        hand.SendMessage("turnOffCollider");
    }

    public void stopAttack()
    {
        m_Animator.SetBool("AttackingGroundpound", false);
        m_Animator.SetBool("AttackingGrounded", false);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

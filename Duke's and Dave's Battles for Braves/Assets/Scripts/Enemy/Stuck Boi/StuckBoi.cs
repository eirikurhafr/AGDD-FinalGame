using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckBoi : MonoBehaviour {

    private Transform target;                                    // target to aim for
    private UserControl[] targetsToKill = new UserControl[2];
    public Transform prefab;
    public float health = 4;
    private bool vulnerable = false;
    private Animator m_Animator;
    public GameObject hand;
    public GameObject head;
    private float battleDistance = 5f;
    private float m_AnimationSpeedPunch = 0.5f;
    private float m_Animation = 1f;
    private float groundPoundChance = 1;
    private float attackCooldown = 0.6f;
    private float attackAirCooldown = 2.7f;
    private float attackDifficulty = 4f;
    private bool dead = false;
    public float attackTimer;
    private float checkTimer;
    private float delayTimer = 0;
    private float realCheckTimer = 1;


    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        targetsToKill[0] = GameObject.Find("Player_1").GetComponent<UserControl>();
        targetsToKill[1] = GameObject.Find("Player_2").GetComponent<UserControl>();
        findClosest();
    }

    public void findClosest()
    {
        float oldDistance = 20f;
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
        if (!dead)
        {
            if(delayTimer <= 0)
            {
                head.GetComponent<Collider>().enabled = false;
                m_Animator.SetBool("Stunned", false);
                delayTimer = Random.Range(1, attackDifficulty);
                if (target != null)
                {
                    if (realCheckTimer <= 0)
                    {
                        findClosest();
                        realCheckTimer = checkTimer;
                    }
                    else
                    {
                        realCheckTimer -= Time.deltaTime;
                    }
                    doAttack();
                }
                else if (target == null)
                {
                    findClosest();
                }
            }
            else
            {
                delayTimer -= Time.deltaTime;
            }
        }
        else
        {
            m_Animator.SetBool("Stunned", false);
            m_Animator.SetBool("AttackingGroundpound", false);
            m_Animator.SetBool("AttackingGrounded", false);
        }
    }

    private void Explosion()
    {
        if (!vulnerable)
        {
            vulnerable = true;
            m_Animator.SetBool("Stunned", true);
            delayTimer = 8f;
            head.GetComponent<Collider>().enabled = true;
        }
    }

    private void hurt(float damage)
    {
        if (vulnerable)
        {
            health -= 1;
            vulnerable = false;
            head.GetComponent<Collider>().enabled = false;
            delayTimer = 0f;
            m_Animator.SetBool("Stunned", false);
            levelUp();
            if (health <= 0)
            {
                m_Animator.SetBool("Death", true);

                Vector3 deadRotate;
                deadRotate.x = -152.4055f;
                deadRotate.y = 0;
                deadRotate.z = -220.0102f;
                transform.rotation = Quaternion.LookRotation(deadRotate);

                dead = true;
            }

        }
    }

    public void levelUp()
    {
        groundPoundChance++;
    }

    public void doAttack()
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = -1;
        transform.rotation = Quaternion.LookRotation(lookPos);
        int number = Random.Range(0, 4);
        if (number < groundPoundChance)
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

    public void spawnSkuli()
    {
        Vector3 temp;
        temp.x = transform.position.x;
        temp.z = transform.position.z;
        temp.y = transform.position.y+4;
        Rigidbody temporary = Instantiate(prefab, temp, Quaternion.identity).GetComponent<Rigidbody>();
        temporary.AddForce(transform.forward*4);
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

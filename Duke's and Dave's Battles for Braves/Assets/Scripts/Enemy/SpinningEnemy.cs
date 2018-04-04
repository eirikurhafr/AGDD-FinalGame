using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningEnemy : MonoBehaviour {

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Character character { get; private set; } // the character we are controlling
    private Transform target;                                    // target to aim for
    private UserControl[] targetsToKill = new UserControl[2];
    public GameObject sword;
    public GameObject explosion;
    public float health = 100;
    public SpawnDamageText damageSpawner;
    public bool dead = false;

    public float damage;
    private Animator m_Animator;
    private Rigidbody rb;
    private SpinningSoundController sound;
    private float battleDistance = 1.5f;
    private float check = 0;
    private bool boolCheck = false;
    private bool distanceCheck = false;


    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        rb = gameObject.GetComponent<Rigidbody>();
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character>();
        agent.stoppingDistance = -1f;
        agent.updateRotation = false;
        agent.updatePosition = true;
        sound = GetComponent<SpinningSoundController>();
        targetsToKill[0] = GameObject.Find("Player_1").GetComponent<UserControl>();
        targetsToKill[1] = GameObject.Find("Player_2").GetComponent<UserControl>();
        m_Animator = GetComponent<Animator>();
        findClosest();
        m_Animator.SetBool("Spinning", true);
        sound.playHellicopter();
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
        if (!dead)
        {
            check += Time.deltaTime;
            if (check + 0.3 >= sound.getHeliLength())
            {
                sound.playHellicopter();
                check = 0;
            }
            if (target != null)
            {
                agent.SetDestination(target.position);
                character.Move(agent.desiredVelocity, false, false);
                if (!distanceCheck)
                {
                    agent.stoppingDistance = 1f;
                    distanceCheck = true;
                }
            }
            else if (target == null)
            {
                findClosest();
            }
        }
        else if(!boolCheck)
        {
            boolCheck = true;
            m_Animator.SetBool("AttackingGrounded", false);
            agent.enabled = false;
            m_Animator.enabled = false;
            tag = "Throwable";
            StartCoroutine("MyCoroutine");
        }
    }

    IEnumerator MyCoroutine()
    {
        rb.position.Set(rb.position.x, 1, rb.position.z);
        rb.velocity = new Vector3(0, 0, 0);
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(1);
        rb.velocity = new Vector3(0, 0, 0);
        hurt(5);
        yield return new WaitForSeconds(1);
        hurt(4);
        yield return new WaitForSeconds(1);
        hurt(3);
        yield return new WaitForSeconds(1);
        hurt(2);
        yield return new WaitForSeconds(1);
        hurt(1);
        yield return new WaitForSeconds(1);
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Destroy(gameObject);
    }

        private void hurt(float damage)
    {
        health -= damage;
        damageSpawner.spawnText(damage.ToString());
        if (health <= 0)
        {
            m_Animator.SetBool("Spinning", false);
            m_Animator.SetBool("Death", true);
            dead = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.name == "Player_1" || other.name == "Player_2") && !dead)
        {
            other.SendMessage("hurtFunction", damage);
        }
    }

    public void doAttack()
    {
        character.Move(Vector3.zero, false, false);
        agent.velocity = Vector3.zero;
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
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

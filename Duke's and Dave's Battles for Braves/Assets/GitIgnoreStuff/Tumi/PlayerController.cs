using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.ThirdPerson;


public class PlayerController : MonoBehaviour {

    private ThirdPersonCharacter m_Character;
    private PlayerController otherPlayer;
    private ThirdPersonUserControl userControl;
    private Animator m_Animator;
    public GameObject sword;
    public GameObject swordAir;
    private bool superJump = false;
    public bool crouching = false;
    private bool dead = false;
    public Rigidbody rb;
    public string controlUse;
    public string controlInteract;
    public string controlDrop;
    public string controlCrouch;
    public string controlJump;
    public string controlAttack;
    private Collider inUseCollider;
    private Rigidbody inUseRB;
    private bool attached = false;
    public GameObject objectInUse;
    public GameObject attachPoint;
    private float health;


    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        userControl = GetComponent<ThirdPersonUserControl>();
        if(m_Character.name == "Player_1")
        {
            otherPlayer = GameObject.Find("Player_2").GetComponent<PlayerController>();
        }
        else
        {
            otherPlayer = GameObject.Find("Player_1").GetComponent<PlayerController>();
        }
        health = 100;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (m_Character.m_IsGrounded)
        {
            swordAir.SetActive(false);
            sword.SetActive(true);
        }

        if (!dead)
        {
            bool interact = CrossPlatformInputManager.GetButtonDown(controlInteract);
            bool drop = CrossPlatformInputManager.GetButton(controlDrop);
            bool use = CrossPlatformInputManager.GetButtonDown(controlUse);
            bool jump = CrossPlatformInputManager.GetButtonDown(controlJump);
            bool attack = CrossPlatformInputManager.GetButtonDown(controlAttack);
            float crouchFloat = CrossPlatformInputManager.GetAxis(controlCrouch);
            bool crouch = false;
            if (crouchFloat < 0)
                crouch = true;
            interactPushed(interact);
            jumpPushed(jump);
            dropPushed(drop);
            crouchPushed(crouch);
            usePushed(use);
            attackPushed(attack);
        }
    }

    public bool getDead()
    {
        return dead;
    }

    private void enableSword()
    {
        sword.SendMessage("turnOnCollider");
    }

    private void disableSword()
    {
        sword.SendMessage("turnOffCollider");
    }

    private void checkForCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Character.transform.position, 1.5f);
        float oldDistance = 10f;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            float newDistance = Vector3.Distance(hitColliders[i].transform.position, m_Character.transform.position);
            if (hitColliders[i].tag == "Throwable")
            {
                if (newDistance < oldDistance)
                {
                    inUseCollider = hitColliders[i];
                    oldDistance = newDistance;
                }
            }
            else if(hitColliders[i].tag == "Player")
            {
                if(otherPlayer.crouching == true)
                {
                    superJump = true;
                }
            }
        }
    }

    public void jumpPushed(bool jump)
    {
        if(jump)
        {
            Collider[] hitColliders = Physics.OverlapSphere(m_Character.transform.position, 1.5f);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].tag == "Player" && otherPlayer.crouching)
                {
                    superJump = true;
                    break;
                }
            }
            m_Character.HandleGroundedMovement(crouching, jump, superJump);
            superJump = false;
        }
    }

    public void crouchPushed(bool crouch)
    {
        userControl.crouch = crouch;
        crouching = crouch;
    }

    //When the player wants to interact with something
    private void interactPushed(bool interact)
    {
        if(interact)
        {
            hurtFunction(101);
        }
        /*if (interact && !attached)
        {
            checkForCollision();
            if (inUseCollider != null)
            {
                if (inUseCollider.tag == "Throwable")
                {
                    inUseCollider.enabled = false;
                    inUseRB = inUseCollider.gameObject.GetComponent<Rigidbody>();
                    inUseRB.isKinematic = true;
                    inUseCollider.transform.rotation = attachPoint.transform.rotation;
                    inUseCollider.transform.position = attachPoint.transform.position;
                    inUseCollider.transform.parent = attachPoint.transform;
                    attached = true;
                }
                else if (inUseCollider.tag == "UseJump" && inUseCollider != otherPlayer.inUseCollider)
                {
                    toggleInUse();
                }
                else if (inUseCollider.tag == "UseJump" && inUseCollider == otherPlayer.inUseCollider)
                {
                    otherPlayer.toggleInUse();
                    inUseCollider = null;
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                }
            }
        }*/
    }

    //When the player wants to drop the item he is carrying
    private void dropPushed(bool drop)
    {
        if(drop && attached)
        {
            inUseCollider.enabled = true;
            inUseRB.isKinematic = false;
            inUseCollider.transform.parent = null;
            attached = false;
            inUseCollider = null;
        }
    }

    private void attackPushed(bool attack)
    {
        if (m_Character.m_IsGrounded)
        {        
            m_Animator.SetBool("AttackingGrounded", attack);
        }
        else if (!m_Character.m_IsGrounded)
        {
            m_Animator.SetBool("AttackingAirborne", attack);
            swordAir.SetActive(true);
            sword.SetActive(false);
        }
    }

    //When the player wants to use the item he is carrying
    private void usePushed(bool use)
    {
        if (use && !attached)
        {
            checkForCollision();
            if (inUseCollider != null)
            {
                if (inUseCollider.tag == "Throwable")
                {
                    inUseCollider.enabled = false;
                    inUseRB = inUseCollider.gameObject.GetComponent<Rigidbody>();
                    inUseRB.isKinematic = true;
                    inUseCollider.transform.rotation = attachPoint.transform.rotation;
                    inUseCollider.transform.position = attachPoint.transform.position;
                    inUseCollider.transform.parent = attachPoint.transform;
                    attached = true;
                }
                else if (inUseCollider.tag == "UseJump" && inUseCollider == otherPlayer.inUseCollider)
                {
                    
                    inUseCollider = null;
                    rb.AddForce(0, 15, 0, ForceMode.Impulse);
                }
            }
        }
        else if (use && attached)
        {
            if (inUseCollider.tag == "Throwable")
            {
                inUseCollider.enabled = true;
                inUseRB.isKinematic = false;
                inUseCollider.transform.parent = null;
                inUseRB.AddForce(transform.forward * 750f);
                attached = false;
                inUseCollider = null;
            }
            else if (inUseCollider.tag == "Attack")
            {
                Debug.Log("Attacking");
            }
            else if (inUseCollider.tag == "Use")
            {
                Debug.Log("Using");
            }
        }
    }

    public void hurtFunction(float healthAltered)
    {
        health -= healthAltered;
        if(health <= 0)
        {
            dead = true;
            userControl.dead = true;
            m_Animator.SetBool("Death", true);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}

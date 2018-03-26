using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

    private Character m_Character;
    private PlayerController otherPlayer;
    private UserControl userControl;
    private SoundController sound;
    private Animator m_Animator;
    public GameObject sword;
    public GameObject swordAir;
    private bool superJump = false;
    public bool crouching = false;
    public List<string> Inventory = new List<string>();
    private bool dead = false;
    public Rigidbody rb;
    public string controlUse;
    public string controlInteract;
    public string controlDrop;
    public string controlCrouch;
    public string controlJump;
    public string controlAttack;
    private Collider inUseCollider;
    private Collider inUseItem;
    private Collider inUseInteract;
    private Rigidbody inUseRB;
    private bool attached = false;
    public GameObject objectInUse;
    public GameObject attachPoint;
    public float health;


    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        m_Character = GetComponent<Character>();
        userControl = GetComponent<UserControl>();
        sound = GetComponent<SoundController>();
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
            m_Animator.SetBool("SuperJump", false);
            bool interact = CrossPlatformInputManager.GetButtonDown(controlInteract);
            bool drop = CrossPlatformInputManager.GetButton(controlDrop);
            bool use = CrossPlatformInputManager.GetButtonDown(controlUse);
            bool jump = CrossPlatformInputManager.GetButtonDown(controlJump);
            bool attack = CrossPlatformInputManager.GetButtonDown(controlAttack);
            float crouchFloat;
            bool crouch = false;

            if(controlCrouch == "Crouch_Keyboard_P1" || controlCrouch == "Crouch_Keyboard_P2")
            {
                crouch = CrossPlatformInputManager.GetButton(controlCrouch);
            }
            else
            {
                crouchFloat = CrossPlatformInputManager.GetAxis(controlCrouch);
                crouch = false;
                if (crouchFloat < 0)
                    crouch = true;
            }
            
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

    private void enableMovement()
    {
        m_Animator.SetBool("Interact", false);
        userControl.lockMovement = false;
    }

    private void enableSword()
    {
        sword.SendMessage("turnOnCollider");
    }

    private void disableSword()
    {
        sword.SendMessage("turnOffCollider");
    }

    private void stopThrow()
    {
        m_Animator.SetBool("Throw", false);
    }

    private void throwItem()
    {
        sound.playThrow();
        inUseItem.enabled = true;
        inUseRB.isKinematic = false;
        inUseItem.transform.parent = null;
        inUseItem.transform.rotation = transform.rotation;
        inUseRB.AddForce(transform.forward * 750f);
        attached = false;
        inUseItem = null;
    }

    private void stopPickup()
    {
        m_Animator.SetBool("PickUp", false);
    }

    private void pickupItem()
    {
        if(inUseItem.tag == "Throwable")
        {
            inUseItem.enabled = false;
            inUseRB = inUseItem.gameObject.GetComponent<Rigidbody>();
            inUseRB.isKinematic = true;
            inUseItem.transform.rotation = attachPoint.transform.rotation;
            inUseItem.transform.position = attachPoint.transform.position;
            inUseItem.transform.parent = attachPoint.transform;
            attached = true;
        }
        else if (inUseItem.tag == "Item" && !Inventory.Contains(inUseItem.name))
        {
            Inventory.Add(inUseItem.name);
            Destroy(inUseItem);
            inUseItem = null;
        }
    }

    private void checkForCollision()
    {
        Collider[] hitColliders = Physics.OverlapSphere(m_Character.transform.position, 1.5f);
        float oldDistanceItem = 10f;
        float oldDistanceInteract = 10f;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            float newDistance = Vector3.Distance(hitColliders[i].transform.position, m_Character.transform.position);
            if (hitColliders[i].tag == "Throwable" || hitColliders[i].tag == "Item")
            {
                if (newDistance < oldDistanceItem)
                {
                    inUseItem = hitColliders[i];
                    oldDistanceItem = newDistance;
                }
            }
            else if(hitColliders[i].tag == "Interact")
            {
                if (newDistance < oldDistanceInteract)
                {
                    inUseInteract = hitColliders[i];
                    oldDistanceInteract = newDistance;
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
                if (hitColliders[i].tag == otherPlayer.tag && otherPlayer.crouching)
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
            checkForCollision();
            if (inUseInteract != null)
            {
                var lookPos = inUseInteract.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
                m_Animator.SetBool("Interact", interact);
                inUseInteract.SendMessage("Use", this);
                userControl.lockMovement = true;
            }
            inUseInteract = null;
        }
    }

    //When the player wants to drop the item he is carrying
    private void dropPushed(bool drop)
    {
        if(drop && attached)
        {
            inUseItem.enabled = true;
            inUseRB.isKinematic = false;
            inUseItem.transform.parent = null;
            attached = false;
            inUseItem = null;
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
            if(attack)
            {
                swordAir.SetActive(true);
                sword.SetActive(false);
            }
        }
    }

    //When the player wants to use the item he is carrying
    private void usePushed(bool use)
    {
        if (use && !attached)
        {
            checkForCollision();
            if (inUseItem != null && (inUseItem.tag == "Throwable" || inUseItem.tag == "Item"))
            {
                sound.playPickup();
                m_Animator.SetBool("PickUp", true);
            }
        }
        else if (use && attached)
        {
            m_Animator.SetBool("Throw", true);
        }
    }

    public void hurtFunction(float healthAltered)
    {
        sound.playHurt();
        health -= healthAltered;
        if(health <= 0)
        {
            dead = true;
            userControl.dead = true;
            m_Animator.SetBool("Death", true);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    private void removeFromInventory(string item)
    {
        Inventory.Remove(item);
    }
}
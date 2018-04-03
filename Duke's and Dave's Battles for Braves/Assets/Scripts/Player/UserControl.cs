using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (Character))]
public class UserControl : MonoBehaviour
{
    private Character m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    private float timer = 0;
    public bool crouch = false;
    public bool inUse = false;
    public bool dead = false;
    private SoundController sound;
    public bool lockMovement = false;
    public string controlHorizontal;
    public string controlVertical;
    public string controlJump;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        sound = GetComponent<SoundController>();
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<Character>();
    }


    private void Update()
    {
        if (!m_Jump && !dead)
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown(controlJump);
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if(!dead && !lockMovement)
        {
            HandleGroundControl();
            HandleAirControl();
        }
    }

    private void HandleAirControl()
    {
        if (!m_Character.getGrounded())
        {
            /*Vertical: 
            -1 down     
                1 up    
            Horizontal:    
                1 right    
            -1 left    
            */        
            float h = CrossPlatformInputManager.GetAxis(controlHorizontal);
            float v = CrossPlatformInputManager.GetAxis(controlVertical);
            rb.AddForce(h*0.1f, 0, v*0.1f, ForceMode.Impulse);
        }
    }

    private void HandleGroundControl()
    {

        float h = 0f, v = 0f;
        if (!inUse)
        {
            h = CrossPlatformInputManager.GetAxis(controlHorizontal);
            v = CrossPlatformInputManager.GetAxis(controlVertical);
            Debug.Log(h);
        }
        // we use world-relative directions in the case of no main camera
        m_Move = v * Vector3.forward + h * Vector3.right;
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        if(h != 0 || v != 0)
        {
            sound.playFootsteps();
        }
        timer -= Time.deltaTime;
        
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }
}

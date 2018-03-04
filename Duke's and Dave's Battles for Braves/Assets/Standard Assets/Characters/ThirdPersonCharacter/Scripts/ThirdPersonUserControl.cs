using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        public GameObject attachPoint;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
        public string controlHorizontal;
        public string controlVertical;
        public string controlJump;
        public string controlUse;
        public string controlDrop;
        private Collider inUseCollider;
        private Rigidbody inUseRB;
        private bool attached = false;

        
        private void Start()
        {
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
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown(controlJump);
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis(controlHorizontal);
            float v = CrossPlatformInputManager.GetAxis(controlVertical);
            bool use = CrossPlatformInputManager.GetButtonDown(controlUse);
            bool drop = CrossPlatformInputManager.GetButton(controlDrop);
            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, false, m_Jump);
            m_Jump = false;

            usePushed(use);

            if(drop && attached)
            {
                dropItem();
            }
        }

        private void checkForCollision()
        {
            Collider[] hitColliders = Physics.OverlapSphere(m_Character.transform.position, 1.5f);
            float oldDistance = 10f;
            for (int i = 0; i < hitColliders.Length; i++)
            {
                float newDistance = Vector3.Distance(hitColliders[i].transform.position, m_Character.transform.position);
                if (hitColliders[i].tag == "Use"|| hitColliders[i].tag == "Attack" || hitColliders[i].tag == "Throwable")
                {
                    if (newDistance < oldDistance)
                    {
                        inUseCollider = hitColliders[i];
                        oldDistance = newDistance;
                    }
                }
            }
            if (inUseCollider != null)
            {
                inUseCollider.enabled = false;
                inUseRB = inUseCollider.gameObject.GetComponent<Rigidbody>();
                inUseRB.isKinematic = true;
                inUseCollider.transform.rotation = attachPoint.transform.rotation;
                inUseCollider.transform.position = attachPoint.transform.position;
                inUseCollider.transform.parent = attachPoint.transform;
                attached = true;
            }
        }

        private void usePushed(bool use)
        {
            if (use && !attached)
            {
                checkForCollision();
            }
            else if(inUseCollider != null)
            {
                if(use && inUseCollider.tag == "Throwable")
                {
                    inUseCollider.enabled = true;
                    inUseRB.isKinematic = false;
                    inUseCollider.transform.parent = null;
                    inUseRB.AddForce(transform.forward*750f);
                    attached = false;
                    inUseCollider = null;
                }
                else if (use && inUseCollider.tag == "Attack")
                {
                    Debug.Log("Attacking");
                }
                else if (use && inUseCollider.tag == "Use")
                {
                    Debug.Log("Using");
                }
            }
        }

        private void dropItem()
        {
            inUseCollider.enabled = true;
            inUseRB.isKinematic = false;
            inUseCollider.transform.parent = null;
            attached = false;
            inUseCollider = null;
        }
    }
}

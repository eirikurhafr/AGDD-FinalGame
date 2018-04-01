using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetScript : MonoBehaviour {
    private Animator m_Animator;
    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
    }

    void SittingGround()
    {
        Debug.Log("Happens");
        ClearBools();
        m_Animator.SetBool("SittingGround", true);
    }

    void Sitting()
    {
        Debug.Log("Happens");
        ClearBools();
        m_Animator.SetBool("Sitting", true);
    }

    void Thinking()
    {
        Debug.Log("Happens");
        ClearBools();
        m_Animator.SetBool("Thinking", true);
    }

    void ClearBools()
    {
        Debug.Log("Happens");
        m_Animator.SetBool("Sitting", false);
        m_Animator.SetBool("SittingGround", false);
        m_Animator.SetBool("Thinking", false);
    }
}

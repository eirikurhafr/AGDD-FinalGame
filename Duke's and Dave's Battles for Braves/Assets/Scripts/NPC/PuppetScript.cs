using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetScript : MonoBehaviour {
    private Animator m_Animator;
    public string startingPose;
    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(startingPose, true);
    }

    void SittingGround()
    {
        ClearBools();
        m_Animator.SetBool("SittingGround", true);
    }

    void Sitting()
    {
        ClearBools();
        m_Animator.SetBool("Sitting", true);
    }

    void Thinking()
    {
        ClearBools();
        m_Animator.SetBool("Thinking", true);
    }

    void ClearBools()
    {
        m_Animator.SetBool("Sitting", false);
        m_Animator.SetBool("SittingGround", false);
        m_Animator.SetBool("Thinking", false);
    }
}

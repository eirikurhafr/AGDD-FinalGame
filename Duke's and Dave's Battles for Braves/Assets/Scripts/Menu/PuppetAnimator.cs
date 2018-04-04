using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetAnimator : MonoBehaviour {
    private Animator m_Animator;
    public string startingPose;
    private float timer = 4;

    private static Random rand  = new Random();

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        m_Animator.SetBool(startingPose, true);
    }

    void Update() {
        if(timer <= 0)
        {
            RandomAnimation();
            timer = 4;
        }
        else
        {
            timer -= Time.deltaTime;
        }
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

    void Pointing()
    {
        ClearBools();
        m_Animator.SetBool("Pointing", true);
    }

    void Waving()
    {
        ClearBools();
        m_Animator.SetBool("Waving", true);
    }

    void Dancing()
    {
        ClearBools();
        m_Animator.SetBool("Dancing", true);
    }

    void Backflip()
    {
        ClearBools();
        m_Animator.SetBool("Backflip", true);
    }

    void Stop()
    {
        ClearBools();
        m_Animator.SetBool("Stop", true);
    }

    void RunFlip()
    {
        ClearBools();
        m_Animator.SetBool("RunFlip", true);
    }

    void HoldingBox()
    {
        ClearBools();
        m_Animator.SetBool("HoldingBox", true);
    }

    void ClearBools()
    {
        m_Animator.SetBool("Stop", false);
        m_Animator.SetBool("HoldingBox", false);
        m_Animator.SetBool("RunFlip", false);
        m_Animator.SetBool("Backflip", false);
        m_Animator.SetBool("Dancing", false);
        m_Animator.SetBool("Pointing", false);
        m_Animator.SetBool("Sitting", false);
        m_Animator.SetBool("Waving", false);
        m_Animator.SetBool("SittingGround", false);
        m_Animator.SetBool("Thinking", false);
    }

    void RandomAnimation()
    {
        int rand = Random.Range(1, 5);
            
        if(rand ==  1)
        {
            ClearBools();
            SittingGround();
        }
        if(rand ==  2)
        {
            ClearBools();
            Backflip();           
        }
        if(rand ==  3)
        {
            ClearBools();
            Dancing();
        }
        if(rand ==  4)
        {
            ClearBools();
            Waving();
        }
        if(rand == 5)
        {
            ClearBools();
            Pointing();
        }

       // Debug.Log(rand);
    }
}
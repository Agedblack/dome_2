using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private KeyboardStats keyboardStats;
    private Animator anim;
    public LayerMask layerMask;
    public Transform GroundCheck;
    public  AttackEvent attackEvent;

    public float Dmag;
    public Vector3 Dvec;
    public Vector3 VelocityG = Vector3.zero;
    public Vector3 planarVec;
    public bool inputEnablad = true;
    public bool lockPlanar = false;
    //public bool attack=false;
    //public bool cut;
    bool IsGround;
    bool jump;
    bool lastJump;
    bool attackL;
    bool lastAttackL;
    bool attackR;
    bool lastAttackR;

    private float Dup;
    private float Dright;
    private float velocityDup;
    private float velocityDright;
    private float gravity = -19.6f;
    private void Awake()
    {
        Transform player = transform.Find("playermod");
        anim = player.GetComponent<Animator>();
        controller =player.GetComponent<CharacterController>();
        keyboardStats = GetComponent<KeyboardStats>();
    }
    private void Update()
    {
        //SwitchhState();
        Gravity();
        
        if (anim.applyRootMotion ==false)
        {
            PlayDirection();
            PlayMove();
            PlayJump(); 
        }
        else
        {
            anim.SetFloat("forward", 0);
            jump = false;
            Dup = 0;
            Dright = 0;
        }
        if (IsGround)
        {
            PlayAttack();
        }
    }

    private void PlayDirection()
    {
        float targetDup = (Input.GetKey(keyboardStats.KeyUp) ? 1.0f : 0) - (Input.GetKey(keyboardStats.KeyDown) ? 1.0f : 0);
        float targetDright = (Input.GetKey(keyboardStats.KeyRight) ? 1.0f : 0) - (Input.GetKey(keyboardStats.KeyLeft) ? 1.0f : 0);
        if (!inputEnablad)
        {
            targetDup = 0;
            targetDright = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);
        Vector2 tempDAxis = PlayerStatic.SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;
        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
        if (Dmag > 0.1f)
        {
            anim.transform.forward = Vector3.Slerp(anim.transform.forward, Dvec, 0.1f);
        }
    }
    private void PlayMove()
    {
        bool run = Input.GetKey(keyboardStats.KeyShift);
        
        float RunSpeed = 4.0f;
        if (!lockPlanar)
        {
            anim.SetFloat("forward", Dmag * ((run)?2.0f:1.0f),0.15f,Time.deltaTime);
            planarVec = Dmag *anim.transform.forward * Time.deltaTime*((run)?RunSpeed:1.0f);
        }
            controller.Move(planarVec);
    }
    private void Gravity()
    {
        float CheckRadius = 0.3f;
        IsGround = Physics.CheckSphere(GroundCheck.position, CheckRadius, layerMask);//地面检测
        VelocityG.y += gravity * Time.deltaTime;
        controller.Move(VelocityG * Time.deltaTime);//重力
    }
    private void KeyDownLike(ref bool newA,ref bool lastA,ref bool A)
    {
        if (newA != lastA && newA)
        {
            A = true;
        }
        else
        {
            A = false;
        }
        lastA = newA;
    }
    private void PlayJump()
    {
        bool newJump = Input.GetKey(keyboardStats.KeySpace);
        KeyDownLike(ref newJump,ref lastJump,ref jump);

        if (IsGround==true&& VelocityG.y < 0)
        {
            anim.SetBool("isGround", true);
            VelocityG.y = 0;
            if (jump)
            {
                anim.SetTrigger("jump");
                float JumpHeight = 2.0f;
                VelocityG.y += Mathf.Sqrt(JumpHeight * -2 * gravity);//近似物理效果的公式
                inputEnablad = false;
                lockPlanar = true;
            }
            else
            {
                inputEnablad = true;
                lockPlanar = false;
            }
        }
        else
        {
            anim.SetBool("isGround", false);
        }
    }
    //private void SwitchhState()
    //{
    //    switch (cut)
    //    {
    //        case false:
    //            if (Input.GetKeyDown(keyboardStats.KeyMouse0))
    //            {
    //                anim.SetTrigger("cut");
    //            }
    //            if (CheckState("equip", "StatuSwitching"))
    //            {
    //                cut = true;
    //            }
    //            break;
    //        case true:
    //            if (CheckState("ground") && CheckState("null", "StatuSwitching"))
    //            {
    //                attack = true;
    //                anim.SetBool("attack", true);
    //            }
    //            break;
    //    }
    //}
    /// <summary>
    /// 检测在某个Base Layer中的状态
    /// </summary>
    /// <param name="stateName"></param>
    /// <param name="layerName"></param>
    /// <returns></returns>
    private bool CheckState(string stateName,string layerName="Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    public void PlayAttack()
    {
        bool newAttackL = Input.GetKey(keyboardStats.KeyMouse0);
        KeyDownLike(ref newAttackL, ref lastAttackL, ref attackL);
        if (CheckState("ground")||CheckState("jump"))
        {
            anim.applyRootMotion = false;
            attackEvent.comboPossibleL = false;
            attackEvent.comboPossibleR = false;
        }
        if (attackL)
        {
                anim.applyRootMotion = true;
                if (CheckState("ground"))
                {
                    anim.Play("combo_01_1");
                }
                else
                {
                    if (attackEvent.comboPossibleL == true)
                    {
                        if (attackEvent.comboPossibleR == true)
                        {
                            attackEvent.comboPossibleR = false;
                        }
                        anim.SetTrigger("attackL");
                        attackEvent.comboPossibleL = false;
                    }
                }
        }

        bool newAttackR = Input.GetKey(keyboardStats.KeyMouse1);
        KeyDownLike(ref newAttackR, ref lastAttackR, ref attackR);
        if (attackR)
        {
            anim.applyRootMotion = true;
            if (CheckState("ground"))
            {
                anim.Play("combo_04_1");
            }
            else
            {
                if (attackEvent.comboPossibleR == true)
                {
                    if (attackEvent.comboPossibleL == true)
                    {
                        attackEvent.comboPossibleL = false;
                    }
                    anim.SetTrigger("attackR");
                    attackEvent.comboPossibleR = false;
                }
            }
        }
    }

}

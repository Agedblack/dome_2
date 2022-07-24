using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    public Animator anim;

    public int comboStepL;
    public int comboStepR;
    public bool comboPossibleL;
    public bool comboPossibleR;
    public void ComboPossible()
    {
     
    }
    public void Combo()
    {
   
    }
    public void ComboReset()
    {
        comboPossibleL = false;
        comboPossibleR = false;
    }
    public void OpenAttackL(string triggerName)
    {
        comboPossibleL = true;
        anim.ResetTrigger(triggerName);
    }
    public void OpenAttackR(string triggerName)
    {
        comboPossibleR = true;
        anim.ResetTrigger(triggerName);
    }
}

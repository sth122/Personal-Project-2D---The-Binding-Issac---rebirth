using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Idle : 멈춘 상태 Anim
// Move : 움직이는 상태 Anim

public class MonsterAnimController : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } private set { animator = value; } }
    public Dictionary<Enum, int> animDic = new Dictionary<Enum, int>();

    private void Awake()
    {
        AnimationInitialize();

        animator = GetComponent<Animator>();
    }

    private void AnimationInitialize()
    {
        animDic[MonsterCurrentState.Move] = Animator.StringToHash("isMove");
        animDic[MonsterCurrentState.Die] = Animator.StringToHash("isDie");
        animDic[MonsterCurrentState.FirstPattern] = Animator.StringToHash("FirstPattern");
        animDic[MonsterCurrentState.SecondPattern] = Animator.StringToHash("SecondPattern");
        animDic[MonsterCurrentState.ThirdPattern] = Animator.StringToHash("ThirdPattern");
    }

    public void AnimationStart(MonsterCurrentState nowAnim)
    {
        animator.SetBool(animDic[nowAnim], true);
    }
    public void AnimationStop(MonsterCurrentState nowAnim)
    {
        animator.SetBool(animDic[nowAnim], false);
    }
}

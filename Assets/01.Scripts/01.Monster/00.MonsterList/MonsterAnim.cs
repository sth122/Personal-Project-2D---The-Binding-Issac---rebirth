using System;
using System.Collections.Generic;
using UnityEngine;

// Idle : 멈춘 상태 Anim
// Move : 움직이는 상태 Anim

public class MonsterAnim : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } private set { animator = value; } }
    public Dictionary<MonsterCurrentState, int> animDic = new Dictionary<MonsterCurrentState, int>();
    #region head animation variable
    public int IsAppear { get; private set; }
    public int IsMove { get; private set; }
    public int IsDie { get; private set; }
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animDic.Clear();
        AnimationInitialize();
    }

    private void AnimationInitialize()
    {
        animDic[MonsterCurrentState.Move] = Animator.StringToHash("isMove");
        animDic[MonsterCurrentState.Die] = Animator.StringToHash("isDie");
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

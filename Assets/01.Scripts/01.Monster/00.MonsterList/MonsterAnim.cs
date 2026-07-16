using System;
using System.Collections;
using UnityEngine;

// Appear : 소환 시 나타내는 Anim
// Idle : 멈춘 상태 Anim
// Move : 움직이는 상태 Anim

public enum MonsterAnimState
{
    Appear, Die
}

public class MonsterAnim : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } private set { animator = value; } }

    #region head animation variable
    public int IsAppear { get; private set; }
    public int IsMove { get; private set; }
    public int IsDie { get; private set; }
    #endregion

    public event Action<bool> OnMoveAnim;
    public void TriggerMove(bool isBoolean)
    {
        OnMoveAnim?.Invoke(isBoolean);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimationInitialize();
    }

    private void OnEnable()
    {
        OnMoveAnim += MoveAnim;
    }
    private void OnDisable()
    {
        OnMoveAnim -= MoveAnim;
    }


    private void MoveAnim(bool isBoolean)
    {
        animator.SetBool(IsMove, isBoolean);
    }

    private void AnimationInitialize()
    {
        IsMove = Animator.StringToHash("isMove");
        IsDie = Animator.StringToHash("isDie");
    }

    public void DeadAnim()
    {
        TriggerMove(false);
        animator.SetBool(IsDie, true);
    }
}

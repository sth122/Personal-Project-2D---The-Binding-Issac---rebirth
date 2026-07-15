using System;
using System.Collections;
using UnityEngine;

// Appear : 소환 시 나타내는 Anim
// Idle : 멈춘 상태 Anim
// Move : 움직이는 상태 Anim

public class MonsterAnim : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } private set { animator = value; } }

    #region head animation variable
    public int IsAppear {  get; private set; }
    public int IsMove { get; private set; }
    #endregion

    public event Action<bool> OnMoveAnim;
    //public event Action OnAppearAnim;
    public void TriggerMove(bool isBoolean)
    {
        OnMoveAnim?.Invoke(isBoolean);
    }

    public void TriggerAppear()
    {
        //OnAppearAnim?.Invoke();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimationInitialize();
    }

    private void OnEnable()
    {
        OnMoveAnim += MoveAnim;
        //OnAppearAnim += AppearAnim;
    }
    private void OnDisable()
    {
        OnMoveAnim -= MoveAnim;
        //OnAppearAnim -= AppearAnim;
    }


    private void MoveAnim(bool isBoolean)
    {
        animator.SetBool(IsMove, isBoolean);
    }

    private void AppearAnim()
    {
        animator.SetBool(IsAppear, true);
    }

    public bool IsAnimationFinished()
    {
        Debug.Log("IsAnimationFinished 진입");
        //animator.Play("Appear");

        Debug.Log("Appear 애님 시작");
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f;
    }

    private void AnimationInitialize()
    {
        IsAppear = Animator.StringToHash("isAppear");
        IsMove = Animator.StringToHash("isMove");
    }
}

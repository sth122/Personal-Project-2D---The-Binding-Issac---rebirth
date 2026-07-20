using System;
using System.Collections.Generic;
using UnityEngine;

public enum IsaacAnimState
{
    Up, Down, LeftRight, Attack, AttackUp, AttackDown, AttackLeftRight, Die, Hit, PickUp, FallDown, Appear
}
public class IsaacAnimController : MonoBehaviour
{
    // Extra, Body, Head를 키 값으로 하는 animDic을 또 만들고 싶은데
    [Header("SpriteRenderers")]
    public SpriteRenderer headSprite;
    public SpriteRenderer bodySprite;

    private IsaacAnimHashData isaacAnimData = new IsaacAnimHashData();
    private Animator animator;

    public Dictionary<IsaacAnimState, int> animDic;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isaacAnimData.Initialize();

        animDic = isaacAnimData.isaacAnimHashData;
    }


    /// <summary>
    /// 1. 상하 입력을 우선 -> 상하 입력 후 좌우 입력 시 -> 좌우 입력 x
    /// 2. 좌우 입력을 차선 -> 좌우 입력 후 상하 입력 시 -> 상하 입력 우선
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="isAttack"></param>
    public void BaseMoveAnim(Vector2 dir)
    {
        bool isUp = dir.y > 0;
        bool isDown = dir.y < 0;
        bool isLeftRight = dir.x != 0;

        animator.SetBool(animDic[IsaacAnimState.Up], isUp);
        animator.SetBool(animDic[IsaacAnimState.Down], isDown);
        animator.SetBool(animDic[IsaacAnimState.LeftRight], isLeftRight);

        if (isLeftRight && bodySprite != null)
        {
            headSprite.flipX = dir.x < 0;
            bodySprite.flipX = dir.x < 0;
        }
    }

    /// <summary>
    /// 1. 상하 입력을 우선 -> 상하 입력 후 좌우 입력 시 -> 좌우 입력 x
    /// 2. 좌우 입력을 차선 -> 좌우 입력 후 상하 입력 시 -> 상하 입력 우선
    /// </summary>
    /// <param name="dir"></param>
    public void HeadMoveAnim(Vector2 dir)
    {
        if (dir == Vector2.zero) { return; }

        bool isUp = dir.y > 0;
        bool isDown = dir.y < 0;
        bool isLeftRight = dir.x != 0;

        animator.SetBool(animDic[IsaacAnimState.AttackUp], isUp);
        animator.SetBool(animDic[IsaacAnimState.AttackDown], isDown);
        animator.SetBool(animDic[IsaacAnimState.AttackLeftRight], isLeftRight);
        
        if (isLeftRight && headSprite != null)
        {
            headSprite.flipX = dir.x < 0;
        }
        //animator.SetTrigger(isAttackHash);
    }

    public void SetAnimTrigger(IsaacAnimState state ,bool isBoolean)
    {
        if (isBoolean)
        {
            animator.SetTrigger(animDic[state]);
        }
        else
        {
            animator.ResetTrigger(animDic[state]);
        }
    }

    public void SetFalseAttackAnim()
    {
        animator.SetBool(animDic[IsaacAnimState.AttackUp], false);
        animator.SetBool(animDic[IsaacAnimState.AttackDown], false);
        animator.SetBool(animDic[IsaacAnimState.AttackLeftRight], false);
    }

    public void FallDownAnim()
    {
        SetAnimTrigger(IsaacAnimState.FallDown, true);
        SetAnimTrigger(IsaacAnimState.Appear, true);
    }
}

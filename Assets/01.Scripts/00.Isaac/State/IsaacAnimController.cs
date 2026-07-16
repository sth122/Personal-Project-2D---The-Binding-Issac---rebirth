using System;
using System.Collections.Generic;
using UnityEngine;

public enum IsaacAnimState
{
    Up, Down, LeftRight, Die
}
public enum IsaacObject
{
    Extra, Body, Head, AttackHead
}
public class IsaacAnimController : MonoBehaviour
{
    // Extra, Body, Head를 키 값으로 하는 animDic을 또 만들고 싶은데
    private IsaacAnimData isaacAnimData = new IsaacAnimData();
    public Dictionary<IsaacObject, Dictionary<IsaacAnimState, int>> animDic;
    public Dictionary<IsaacObject, Animator> animatorDic = new();
    public Dictionary<IsaacObject, SpriteRenderer> spriteDic = new();
    public Dictionary<IsaacObject, GameObject> childDic = new();
    private IsaacObject isaacObj;

    private void Awake()
    {
        isaacAnimData.Initialize();
        animDic = isaacAnimData.isaacAnimDic;
        Init();
    }

    private void Init()
    {
        foreach (Transform child in transform)
        {
            if (Enum.TryParse<IsaacObject>(child.name, true, out IsaacObject obj))
            {
                if (child.TryGetComponent<Animator>(out var animator))
                {
                    animatorDic[obj] = animator;
                }
                if (child.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
                {
                    spriteDic[obj] = spriteRenderer;
                }
                childDic[obj] = child.gameObject;
            }
        }
    }


    public void AnimationStart(IsaacObject obj, IsaacAnimState nowAnim)
    {
        animatorDic[obj].SetBool(animDic[obj][nowAnim], true);
    }
    public void AnimationStop(IsaacObject obj, IsaacAnimState nowAnim)
    {
        animatorDic[obj].SetBool(animDic[obj][nowAnim], false);
    }

    public void SetActiveHead(IsaacObject obj, bool isBool)
    {
        childDic[obj].SetActive(isBool);
    }

    /// <summary>
    /// 1. 상하 입력을 우선 -> 상하 입력 후 좌우 입력 시 -> 좌우 입력 x
    /// 2. 좌우 입력을 차선 -> 좌우 입력 후 상하 입력 시 -> 상하 입력 우선
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="isAttack"></param>
    public void HeadMoveAnim(Vector2 dir)
    {
        isaacObj = IsaacObject.Head;
        OnExcuteAnim(dir, isaacObj);
    }

    /// <summary>
    /// 1. 상하 입력을 우선 -> 상하 입력 후 좌우 입력 시 -> 좌우 입력 x
    /// 2. 좌우 입력을 차선 -> 좌우 입력 후 상하 입력 시 -> 상하 입력 우선
    /// </summary>
    /// <param name="dir"></param>
    public void BodyMoveAnim(Vector2 dir)
    {
        isaacObj = IsaacObject.Body;
        OnExcuteAnim(dir, isaacObj);
    }

    public void AttacHeadAnim(Vector2 dir)
    {


        isaacObj = IsaacObject.AttackHead;
        OnExcuteAnim(dir, isaacObj);
    }

    private void OnExcuteAnim(Vector2 dir, IsaacObject obj)
    {
        if (dir.y > 0)
        {
            AnimationStart(obj, IsaacAnimState.Up);
            AnimationStop(obj, IsaacAnimState.Down);
            AnimationStop(obj, IsaacAnimState.LeftRight);
            return;
        }
        else if (dir.y < 0)
        {
            AnimationStart(obj, IsaacAnimState.Down);
            AnimationStop(obj, IsaacAnimState.Up);
            AnimationStop(obj, IsaacAnimState.LeftRight);
            return;
        }
        else
        {
            AnimationStop(obj, IsaacAnimState.Up);
            AnimationStop(obj, IsaacAnimState.Down);
        }

        if (dir.x != 0)
        {
            if (spriteDic.TryGetValue(isaacObj, out var sprite))
            {
                sprite.flipX = dir.x < 0;
            }
            AnimationStart(obj, IsaacAnimState.LeftRight);
        }
        else
        {
            if (spriteDic.TryGetValue(isaacObj, out var sprite))
            {
                sprite.flipX = false;
            }
            AnimationStop(obj, IsaacAnimState.LeftRight);
        }
    }

}

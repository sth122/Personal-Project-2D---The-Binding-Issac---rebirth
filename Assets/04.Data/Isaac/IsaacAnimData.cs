using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IsaacAnimData
{
    public Dictionary<IsaacObject, Dictionary<IsaacAnimState, int>> isaacAnimDic = new Dictionary<IsaacObject, Dictionary<IsaacAnimState, int>>();

    public void Initialize()
    {
        // Extra Anim
        Dictionary<IsaacAnimState, int> extraAnimDic = new Dictionary<IsaacAnimState, int>();
        extraAnimDic[IsaacAnimState.Die] = Animator.StringToHash("isDie");
        isaacAnimDic[IsaacObject.Extra] = extraAnimDic;


        foreach(IsaacObject obj in Enum.GetValues(typeof(IsaacObject)))
        {
            isaacAnimDic[obj] = ForeachDic();
        }

        //// Head Anim
        //Dictionary<IsaacAnimState, int> headAnimDic = ForeachDic();
        //isaacAnimDic[IsaacObject.Head] = headAnimDic;

        //// Body
        //Dictionary<IsaacAnimState, int> bodyAnimDic = ForeachDic();
        //isaacAnimDic[IsaacObject.Body] = bodyAnimDic;

        //// Attack Head
        //Dictionary<IsaacAnimState, int> attackHeadAnim = ForeachDic();
        //isaacAnimDic[IsaacObject.AttackHead] = attackHeadAnim;
    }

    private Dictionary<IsaacAnimState, int> ForeachDic()
    {
        Dictionary<IsaacAnimState, int> dic = new Dictionary<IsaacAnimState, int>();
        dic[IsaacAnimState.Up] = Animator.StringToHash("isUp");
        dic[IsaacAnimState.Down] = Animator.StringToHash("isDown");
        dic[IsaacAnimState.LeftRight] = Animator.StringToHash("isLeftRight");

        return dic;
    }
}

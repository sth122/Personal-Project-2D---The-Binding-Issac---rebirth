using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IsaacAnimHashData
{
    public Dictionary<IsaacAnimState, int> isaacAnimHashData = new Dictionary<IsaacAnimState, int>();

    public void Initialize()
    {
        isaacAnimHashData[IsaacAnimState.Up] = Animator.StringToHash("isUp");
        isaacAnimHashData[IsaacAnimState.Down] = Animator.StringToHash("isDown");
        isaacAnimHashData[IsaacAnimState.LeftRight] = Animator.StringToHash("isLeftRight");
        isaacAnimHashData[IsaacAnimState.AttackUp] = Animator.StringToHash("isAttackUp");
        isaacAnimHashData[IsaacAnimState.AttackDown] = Animator.StringToHash("isAttackDown");
        isaacAnimHashData[IsaacAnimState.AttackLeftRight] = Animator.StringToHash("isAttackLeftRight");
        isaacAnimHashData[IsaacAnimState.Die] = Animator.StringToHash("isDie");
    }
}

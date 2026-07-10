using System;
using UnityEngine;

[Serializable]
public class IsaacAinmData
{
    [SerializeField] private string moveParameterName = "isMove";
    [SerializeField] private string upDownParameterName = "isUpDown";
    [SerializeField] private string attackParameterName = "isAttack";

    public int MoveParameterHash { get; private set; }
    public int UpDownParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }

    public void Initialize()
    {
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        UpDownParameterHash = Animator.StringToHash(upDownParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
    }
}

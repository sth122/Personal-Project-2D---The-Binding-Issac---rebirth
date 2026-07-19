using System;
using UnityEngine;

/// <summary>
/// 매니저에서 해야하는 일
/// 1. 스탯 연산
/// 2. 피해 연산
/// 3. 아이템 사용
/// </summary>
public class IsaacManager : Singleton<IsaacManager>
{
    public IsaacData isaacData;
    [SerializeField] IsaacInfo isaacInfo;

    private void Awake()
    {
        isaacInfo = isaacData.isaacList[0].Clone();
    }

    public IsaacInfo GameStart(Action OnAction)
    {
        OnAction?.Invoke();
        return isaacInfo;
    }


    public void TakeDamage(float damage, Action OnDie)
    {
        isaacInfo.hp -= damage;
        if (isaacInfo.hp <= 0)
        {
            isaacInfo.hp = 0;
            OnDie?.Invoke();
        }
    }

    public float ApplyDamage()
    {
        return isaacInfo.damage;
    }

    private void SetDamage()
    {
        // 아이템 계산 적용
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 매니저에서 해야하는 일
/// 1. 스탯 연산 + 피해 연산 + 아이템
/// 
/// 3. 아이템 사용
/// </summary>
public class IsaacManager : Singleton<IsaacManager>
{
    [SerializeField] public GameObject Player;
    [SerializeField] IsaacInfo currentIsaacInfo;
    public IsaacData isaacData;
    private bool isDie;
    private float maxHP;
    private Dictionary<PickUpType, int> pickUpDic = new();

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Init();
    }
    public void Init()
    {
        // 메인 메뉴에서 캐릭터 선택 시 초기화 하는 방향으로 추후 수정
        currentIsaacInfo = DataManager.Instance.IsaacData.isaacList[0].Clone();
        maxHP = currentIsaacInfo.hp;

        // 원래 캐릭터 별로 초기 소유 값이 다르지만 빠른 진행을 위해 1,1,1로 통일
        foreach(PickUpType type in Enum.GetValues(typeof(PickUpType)))
        {
            pickUpDic[type] = 1;
        }
    }

    public IsaacInfo GameStart()
    {
        isDie = false;
        return currentIsaacInfo;
    }


    public void TakeDamage(float damage, Action OnDie)
    {
        if (isDie) return;

        Debug.Log("데미지");
        currentIsaacInfo.hp -= damage;
        if (currentIsaacInfo.hp <= 0)
        {
            currentIsaacInfo.hp = 0;
            isDie = true;
            OnDie?.Invoke();
        }
    }

    /// <summary>
    /// 아이작의 데미지를 눈물에 적용 시키는 메서드
    /// </summary>
    /// <returns></returns>
    public float ApplyDamage()
    {
        return currentIsaacInfo.damage;
    }

    public void IsaacDie()
    {
        // UIManager에서 연결
    }
    private void SetDamage()
    {
        // 아이템 계산 적용
    }

    #region Item Method
    public void GetItem(Action OnGetItem) 
    {
        OnGetItem?.Invoke();
    }
    #endregion

    #region HPRecovery
    public void HPRecovery(float effect, Action OnRecovery)
    {
        Debug.Log($"HP {effect} 회복");

        currentIsaacInfo.hp += effect;
        if (currentIsaacInfo.hp >= maxHP)
        {
            currentIsaacInfo.hp = maxHP;
            OnRecovery?.Invoke();
        }

        Debug.Log($"{currentIsaacInfo.hp}");
        // UI 관련 호출
    }

    public void HPCheck(Action OnRecovery)
    {
        Debug.Log($"{currentIsaacInfo.hp}");
        if (currentIsaacInfo.hp < maxHP)
        {
            OnRecovery?.Invoke();
        }
        else
        {
            Debug.Log("체력 만땅");
        }
    }
    #endregion

    #region Item PickUp Method
    public void GetPickUpItem(PickUpType type,int count, Action OnGetPickUpItem)
    {
        // 최대 소유 개수 99개
        if (pickUpDic[type] >= 99)
        {
            return;
        }
        else
        {
            pickUpDic[type] += count;
            Debug.Log($"{type} {pickUpDic[type]}");
            OnGetPickUpItem?.Invoke();
        }
    }
    #endregion
}

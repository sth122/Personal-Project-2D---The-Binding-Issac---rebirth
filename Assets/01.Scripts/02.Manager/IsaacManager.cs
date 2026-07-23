using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 매니저에서 해야하는 일
/// 1. 스탯 연산
/// 2. 피해 연산
/// 3. 아이템 사용
/// </summary>
public class IsaacManager : Singleton<IsaacManager>
{
    [SerializeField] public GameObject Player;
    [SerializeField] IsaacInfo currentIsaacInfo;
    public IsaacData isaacData;
    private bool isDie;
    private Dictionary<PickUpType, int> pickUpItems = new();
    private float maxHP;

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

        // 초기 픽업 아이템 초기화
        foreach (PickUpType type in Enum.GetValues(typeof(PickUpType)))
        {
            Debug.Log($"{pickUpItems[type]} {type.ToString()} = 0");
            pickUpItems[type] = 0;
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
    public void GetItem() { }
    #endregion

    public void HPRecovery(float effect)
    {
        currentIsaacInfo.hp += effect;
        if(currentIsaacInfo.hp > maxHP)
        {
            currentIsaacInfo.hp = maxHP;
        }
        // UI 관련 호출
    }
}

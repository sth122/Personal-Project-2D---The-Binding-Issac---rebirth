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
    [SerializeField] public GameObject Player;
    public IsaacData isaacData;
    [SerializeField] IsaacInfo isaacInfo;
    //[SerializeField] 
    private bool isDie;
    private int keyCount;
    private int bombCount;


    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Initialize()
    {
        //Init();
    }


    private void Start()
    {
        Init(); 
    }
    public void Init()
    {
        isaacInfo = DataManager.Instance.IsaacData.isaacList[0].Clone();
    }


    public IsaacInfo GameStart()
    {
        isDie = false;
        return isaacInfo;
    }


    public void TakeDamage(float damage, Action OnDie)
    {
        if (isDie) return;

        isaacInfo.hp -= damage;
        if (isaacInfo.hp <= 0)
        {
            isaacInfo.hp = 0;
            isDie = true;
            OnDie?.Invoke();
        }
    }

    public float ApplyDamage()
    {
        return isaacInfo.damage;
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
    public void GetItem()
    {

    }
    #endregion
}

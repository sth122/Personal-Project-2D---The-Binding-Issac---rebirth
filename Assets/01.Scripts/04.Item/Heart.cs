using UnityEngine;

public class Heart : Item, IChangeStat, IReturnPool
{
    protected float recoveryHpEffect;

    protected void Start()
    {
         
        recoveryHpEffect = 2;
    }

    protected override void ItemEffect()
    {
        IsaacManager.Instance.HPRecovery(recoveryHpEffect);
    }

    public void ChangeStat()
    {
        
    }

    public void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnObject("Heart", this.gameObject);
    }
}

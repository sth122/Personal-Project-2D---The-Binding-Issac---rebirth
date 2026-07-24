using UnityEngine;

public class Heart : Item, IChangeStat, IReturnPool
{
    protected float recoveryHpEffect;

    protected void Start()
    {
        itemName = "Heart";
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

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac"))
        {
            IsaacManager.Instance.HPCheck(
                () =>
                {
                    ItemEffect();
                    ReturnPool();
                });
        }
    }
}

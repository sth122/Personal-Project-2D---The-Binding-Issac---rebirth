using UnityEngine;

public class AttackFly : Fly , IAttackable
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnDataLodead()
    {
        Debug.Log($"Attack Fly 세팅 {mData}");
    }
    public float ContactAttack()
    {
        return 0;
    }
    public void Attack()
    {

    }

    public override void Trace()
    {
        base.Trace();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    public override void ReturnPool()
    {
        base.ReturnPool();
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}

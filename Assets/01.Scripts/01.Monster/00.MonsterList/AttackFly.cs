using System;
using UnityEngine;
using System.Collections;
public class AttackFly : Fly
{
    protected float attackRange;

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

    public override void TakeDamage(float damage, Vector2 damageDir)
    {
        base.TakeDamage(damage, damageDir);
    }

    public override void Knockback(Vector2 damageDir)
    {
        base.Knockback(damageDir);
    }
    public override void ReturnPool()
    {
        base.ReturnPool();
    }

    public override IEnumerator HitFlash(Action OnKnockback)
    {
        return base.HitFlash(OnKnockback);
    }
}

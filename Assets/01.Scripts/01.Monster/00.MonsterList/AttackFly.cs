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
}

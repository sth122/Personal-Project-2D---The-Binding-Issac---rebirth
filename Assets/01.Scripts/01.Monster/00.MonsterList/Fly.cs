using System.Collections;
using UnityEngine;

public class Fly : MonsterController, ITraceable
{
    protected float traceRange;

    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Trace] = new MonsterTraceState(this, mData);

        knockbackForce = 2f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
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
        Debug.Log($"Fly 세팅 {mData}");
    }

    #region Trace
    private bool CheckDistance()
    {
        // Isaac과의 거리 계산
        float distance = Vector3.Distance(transform.position, target.position);

        return distance < traceRange;
    }

    public virtual void Trace()
    {
        Debug.Log("트레이스 진입");
        if (isKnockback)
            return;

        sr.flipX = CheckFlip();
        Move();
    }

    private bool CheckFlip()
    {
        return transform.position.x > target.position.x;
    }

    private Vector2 GetDirection()
    {
        return (target.position - transform.position).normalized;
    }
    private void Move()
    {
        rb.linearVelocity = GetDirection() * mData.speed;
    }
    #endregion
}

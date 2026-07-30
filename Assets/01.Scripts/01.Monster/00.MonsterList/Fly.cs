using System.Collections;
using UnityEngine;

public class Fly : MonsterController, ITraceable
{
    protected float traceRange;

    protected override void Awake()
    {
        base.Awake();
        knockbackForce = 5f;
        mStateDic[MonsterCurrentState.Trace] = new MonsterTraceState(this, mData);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Movement());
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
    protected virtual void Move()
    {
        rb.linearVelocity = GetDirection() * mData.speed;
    }

    IEnumerator Movement()
    {
        Vector3 originPos = transform.position;
        while (true)
        {
            if(target != null)
            {
                originPos = Vector3.MoveTowards(originPos, target.position, mData.speed * Time.deltaTime);
            }

            float x = Random.Range(-0.1f, 0.1f);
            float y = Random.Range(-0.1f, 0.1f);

            transform.position = originPos + new Vector3(x, y, 0);
            yield return null;
        }
    }
    public virtual void StopMovement()
    {
        StopCoroutine(Movement());
    }
    #endregion
}

using UnityEngine;

public class Fly : MonsterController, ITraceable, ITakeDamage
{
    protected override void Awake()
    {
        base.Awake();
        mStateDic[MonsterCurrentState.Trace] = new MonsterTraceState(this, mData);
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

        return distance < mData.traceRange;
    }

    public void Trace()
    {
        Debug.Log("트레이스 진입");

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
        RB.linearVelocity = GetDirection() * mData.speed;
    }
    #endregion

    public void TakeDamage(int damage)
    {
        mData.totalHp -= damage;
        if (mData.totalHp <= 0)
        {
            mData.totalHp = 0;
            Dead();
        }
    }

    public override void ReturnPool()
    {
        ObjectPoolManager.Instance.ReturnObject(mData.name, this.gameObject);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Isaac Bullet"))
        {
            TakeDamage(1);
        }
    }

}

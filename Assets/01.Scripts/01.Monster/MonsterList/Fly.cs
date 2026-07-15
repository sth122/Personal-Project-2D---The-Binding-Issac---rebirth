using UnityEngine;

public class Fly : MonsterController, ITraceable
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
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
        Debug.Log($"Fly 세팅 {mStat}");
    }

    public void CheckDistance()
    {
        // Isaac과의 거리 계산
        float distance = Vector3.Distance(transform.position, target.position);

        bool isRangeIn = distance < mStat.traceRange;
        if (isRangeIn)
        {
            Trace();
        }
    }

    public void Trace()
    {
        sr.flipX = CheckFlip();
        Vector3 dir = GetDirection();
    }

    private bool CheckFlip()
    {
        return transform.position.x > target.position.x;
    }

    private Vector3 GetDirection()
    {
        return (transform.position - target.position).normalized;
    }
}

using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] Transform[] firePosition;
    [SerializeField] GameObject tearBullet;
    Transform leftOriginPos;
    Transform rightOriginPos;

    protected override void Start()
    {
        base.Start();
        leftOriginPos = firePosition[0].transform;
        rightOriginPos = firePosition[1].transform;
    }

    protected void Update()
    {
        LookHead();
        //CheckDirection();
        Attack();
    }

    protected override void Attack()
    {

    }

    //private HeadDirection CheckDirection()
    //{
    //    if (attackDirection == Vector2.zero)
    //        return HeadDirection.Down;

    //    switch

    //}

    private void TransPosEyes()
    {
        // 정면일 경우 그대로
        // 좌우일 경우 눈동자 x위치는 동일, y좌표 조정
    }
}

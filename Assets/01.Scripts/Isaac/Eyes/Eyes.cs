using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] GameObject tearBullet;
    [SerializeField] Transform fire;
    Transform currentFirePos;
    Transform originFirePos;
    Transform leftOriginPos;
    Transform rightOriginPos;

    protected override void Start()
    {
        base.Start();

        originFirePos = fire;
        //leftOriginPos = firePosition[0].transform;
        //rightOriginPos = firePosition[1].transform;
    }

    protected void Update()
    {
        LookHead();
        //CheckDirection();
        Attack();
    }

    protected override void Attack()
    {
        if (attackDirection == Vector2.zero)
            return;


        GameObject tearBullet = ObjectPoolManager.Instance.GetObject("IsaacBullet");
        tearBullet.transform.position = fire.position;
        tearBullet.transform.rotation = fire.rotation;
        canAttack = false;

    }

    private void CheckDirection()
    {
        if (attackDirection == Vector2.zero)
            transform.position = originFirePos.position;

        //switch

    }

    private void TransPosEyes()
    {
        // 정면일 경우 그대로
        // 좌우일 경우 눈동자 x위치는 동일, y좌표 조정
    }
}

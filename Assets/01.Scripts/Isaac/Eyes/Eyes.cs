using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] GameObject tearBullet;
    [SerializeField] Transform fire;
    [SerializeField]Vector3 originFirePos;

    protected override void Start()
    {
        base.Start();

        originFirePos = fire.position;
    }

    protected void Update()
    {
        LookHead();
        Attack();
    }

    protected override void Attack()
    {
        if (attackDirection != Vector2.zero && canAttack)
        {
            CheckDirection();

            GameObject tearBullet = ObjectPoolManager.Instance.GetObject("IsaacBullet");
            tearBullet.transform.position = fire.position;
            tearBullet.transform.rotation = fire.rotation;
            canAttack = false;
        }
    }

    private void CheckDirection()
    {
        //if (attackDirection == Vector2.zero)
        //    fire.position = originFirePos.position;

        switch (Input.CurrentHeadDirection)
        {
            case HeadDirection.Left:
                fire.localPosition = new Vector3(-0.25f, -0.25f * posDir, 0f);
                break;
            case HeadDirection.Right:
                fire.localPosition = new Vector3(0.25f, -0.25f * posDir, 0f);
                break;
            case HeadDirection.Up:
                fire.localPosition = new Vector3(0.25f * posDir, 0.25f, 0f);
                break;
            case HeadDirection.Down:
                fire.localPosition = new Vector3(0.25f * posDir, -0.25f, 0f);
                break;
        }

    }

    private void TransPosEyes()
    {
        // 정면일 경우 그대로
        // 좌우일 경우 눈동자 x위치는 동일, y좌표 조정
    }
}

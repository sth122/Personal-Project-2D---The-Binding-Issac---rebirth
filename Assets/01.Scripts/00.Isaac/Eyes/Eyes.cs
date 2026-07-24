using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] Transform fire;
    [SerializeField] Vector3 originFirePos;
    private string bulletName;
    private int tearScale;
    private TearType type;

    protected override void Start()
    {
        base.Start();
        type = TearType.BasicTears;
        originFirePos = fire.localPosition;
        tearScale = 0;
    }

    protected override void Update()
    {
        Attack();
    }

    protected override void Attack()
    {
        if (Input.AttackDirection != Vector2.zero && canAttack)
        {
            CheckDirection();

            GameObject tearBullet = SpawnManager.Instance.SpawnBullet(type);
            if (tearBullet != null)
            {
                IsaacBullet bullet = tearBullet.GetComponent<IsaacBullet>();

                bullet.SetDirection(Input.AttackDirection);
                bullet.transform.position = fire.position;
                bullet.transform.rotation = fire.rotation;
                canAttack = false;
            }
            else
            {
                Debug.Log("Bullet null error");
            }
        }
    }

    /// <summary>
    /// 나중에 눈물 변경 로직 추가 ( BloodTear / 혈사(구현할진 모름) )
    /// </summary>
    private void SetTears(TearType type, int tearScale)
    {
        this.type = type;
        this.tearScale = tearScale;
    }

    /// <summary>
    /// 눈물 발사 지점
    /// 현재는 하드 코딩 -> 아이템 획득에 따른 눈물 포지션 변경 예정
    /// </summary>
    private void CheckDirection()
    {
        switch (Input.CurrentHeadDirection)
        {
            case AttackInputDirection.Left:
                fire.localPosition = new Vector3(-originFirePos.x, originFirePos.y + (-0.05f) * posDir, 0f);
                break;
            case AttackInputDirection.Right:
                fire.localPosition = new Vector3(originFirePos.x, originFirePos.y + (-0.05f) * posDir, 0f);
                break;
            case AttackInputDirection.Up:
                fire.localPosition = new Vector3(originFirePos.x * posDir, originFirePos.x, 0f);
                break;
            case AttackInputDirection.Down:
                fire.localPosition = new Vector3(originFirePos.x * posDir, -originFirePos.x, 0f);
                break;
        }
    }
}
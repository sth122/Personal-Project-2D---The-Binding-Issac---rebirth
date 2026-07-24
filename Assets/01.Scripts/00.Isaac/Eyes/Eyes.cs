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
        originFirePos = fire.localPosition;
        type = TearType.BasicTears;
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
    /// Updates the fire object's local position based on the current head direction input.
    /// </summary>
    private void CheckDirection()
    {
        switch (Input.CurrentHeadDirection)
        {
            case AttackInputDirection.Left:
                fire.localPosition = new Vector3(-originFirePos.x, -originFirePos.y * posDir, 0f);
                break;
            case AttackInputDirection.Right:
                fire.localPosition = new Vector3(originFirePos.x, -originFirePos.y * posDir, 0f);
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
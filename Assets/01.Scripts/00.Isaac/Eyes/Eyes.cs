using Unity.VisualScripting;
using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] GameObject tearBullet;
    [SerializeField] Transform fire;
    [SerializeField] Vector3 originFirePos;
    private string bulletName;

    protected override void Start()
    {
        base.Start();
        bulletName = tearBullet.name;
        originFirePos = fire.localPosition;
    }

    protected void Update()
    {
        Attack();
    }

    protected override void Attack()
    {
        if (attackDirection != Vector2.zero && canAttack)
        {
            CheckDirection();

            var tearBullet = ObjectPoolManager.Instance.GetObject(bulletName);
            if (tearBullet != null)
            {
                IsaacBullet bullet = tearBullet.GetComponent<IsaacBullet>();

                bullet.SetDirection(attackDirection);
                bullet.transform.position = fire.position;
                bullet.transform.rotation = fire.rotation;
                canAttack = false;
            }
        }
    }

    /// <summary>
    /// 나중에 눈물 변경 로직 추가 ( BloodTear / 혈사(구현할진 모름) )
    /// </summary>
    private void SetTears() { }

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

/*
 
                5                           if T > T_max

                16 - 6 x sqrt(T x 1.3 + 1)  if T ≥ 0 and  T ≤ T_max
Tear Delay = 
                16 - 6 x sqrt(T x 1.3 + 1) - 6 x T  if T < 0 and  T > -0.77

                16 - 6 x T                  if T ≤ -0.77
 */
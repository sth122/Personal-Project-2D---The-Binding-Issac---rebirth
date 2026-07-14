using UnityEngine;

public class Eyes : IsaacWeapon
{
    [SerializeField] GameObject tearBullet;
    [SerializeField] Transform fire;
    [SerializeField] Vector3 originFirePos;

    protected override void Start()
    {
        base.Start();

        originFirePos = fire.localPosition;
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
            tearBullet.GetComponent<IsaacBullet>().SetDirection(attackDirection);
            tearBullet.transform.position = fire.position;
            tearBullet.transform.rotation = fire.rotation;
            canAttack = false;
        }
    }

    /// <summary>
    /// Updates the fire object's local position based on the current head direction input.
    /// </summary>
    private void CheckDirection()
    {
        switch (Input.CurrentHeadDirection)
        {
            case HeadDirection.Left:
                fire.localPosition = new Vector3(-originFirePos.x, -originFirePos.y * posDir, 0f);
                break;
            case HeadDirection.Right:
                fire.localPosition = new Vector3(originFirePos.x, -originFirePos.y * posDir, 0f);
                break;
            case HeadDirection.Up:
                fire.localPosition = new Vector3(originFirePos.x * posDir, originFirePos.x, 0f);
                break;
            case HeadDirection.Down:
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
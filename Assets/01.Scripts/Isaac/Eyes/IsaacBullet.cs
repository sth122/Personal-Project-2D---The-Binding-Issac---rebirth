using UnityEngine;

public class IsaacBullet : MonoBehaviour, IReturnPoolable
{
    #region variable
    private Rigidbody2D rb;
    private Vector2 dir;
    private float lifeTime;
    private float speed;
    [SerializeField]private float timer;
    private float damage;

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        timer = 0f;
    }

    private void Start()
    {
        lifeTime = 2f;
        speed = 5f;
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        rb.linearVelocity = dir * speed;
    }

    private void Update()
    {
        if (timer > lifeTime)
        {
            ReturnPool();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            //collision.gameObject.GetCompnent<MonsterController>().TakeDamage(damage);
            ReturnPool();
        }
    }

    /// <summary>
    /// 캐릭터 눈물 공식 계산 후 적용
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(float damage) { this.damage = damage; }

    /// <summary>
    /// 캐릭터 공격 방향 벡터 설정
    /// </summary>
    /// <param name="dir"></param>
    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }
   public void ReturnPool()
    {
        // 눈물 사운드 이팩트 추가
        // 눈물 충돌 이펙트(anim) 추가
        ObjectPoolManager.Instance.ReturnObject("IsaacBullet", this.gameObject);
    }
}

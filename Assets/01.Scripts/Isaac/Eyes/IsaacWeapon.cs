using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// Player 무기 추상 클래스 스크립트 = Player's weapon abstract class script
/// </summary>
abstract public class IsaacWeapon : MonoBehaviour
{
    #region protected Variable
    [SerializeField] protected int damage;
    [SerializeField] GameObject isaac;

    protected float tearDelay;
    protected bool canAttack;
    private Animator headAnimator;
    public IsaacInput Input { get; private set; }
    private WaitForSeconds wait;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    protected Vector2 attackDirection;
    protected HeadDirection headDirection;
    protected int posDir;
    private bool isAttack;
    #endregion

    #region head animation variable
    public int IsHeadMove { get; private set; }
    public int IsHeadUp { get; private set; }
    public int IsHeadLeftRightAttack { get; private set; }
    public int IsHeadUpAttack { get; private set; }
    public int IsHeadDownAttack { get; private set; }
    #endregion

    private void Awake()
    {
        headAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Input = isaac.GetComponent<IsaacInput>();
        rb = isaac.GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        AnimationInitialize();

        canAttack = true;
        tearDelay = 0.5f;
        wait = new WaitForSeconds(tearDelay);
        posDir = 1;
        StartCoroutine(AttackDelay());
    }

    protected abstract void Attack();

    protected void LookHead()
    {
        attackDirection = Input.AttackDirection;
        isAttack = attackDirection != Vector2.zero;

        if (isAttack)
        {
            AnimationAttack(Input.CurrentHeadDirection);
        }
        else
        {
            moveDirection = rb.linearVelocity;
            AnimationMove(moveDirection);
        }
    }

    IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => canAttack);
            yield return wait;
            posDir *= -1;
            canAttack = true;
        }
    }

    private void AnimationInitialize()
    {
        IsHeadMove = Animator.StringToHash("isHeadMove");
        IsHeadUp = Animator.StringToHash("isHeadUp");
        IsHeadLeftRightAttack = Animator.StringToHash("isHeadLeftRightAttack");
        IsHeadUpAttack = Animator.StringToHash("isHeadUpAttack");
        IsHeadDownAttack = Animator.StringToHash("isHeadDownAttack");
    }
    private void AnimationAttack(HeadDirection dir)
    {
        AnimationAttackInit();
        switch (dir)
        {
            case HeadDirection.Left:
                headAnimator.SetBool(IsHeadLeftRightAttack, true);
                sr.flipX = true;
                break;
            case HeadDirection.Right:
                headAnimator.SetBool(IsHeadLeftRightAttack, true);
                sr.flipX = false;
                break;
            case HeadDirection.Up:
                headAnimator.SetBool(IsHeadUpAttack, true);
                break;
            case HeadDirection.Down:
                headAnimator.SetBool(IsHeadDownAttack, true);
                break;
        }

        //if (dir.y > 0)
        //{
        //    headAnimator.SetBool(IsHeadUpAttack, true);
        //    headAnimator.SetBool(IsHeadDownAttack, false);
        //    headAnimator.SetBool(IsHeadLeftRightAttack, false);
        //    return;
        //}
        //else if (dir.y < 0)
        //{
        //    headAnimator.SetBool(IsHeadUpAttack, false);
        //    headAnimator.SetBool(IsHeadDownAttack, true);
        //    headAnimator.SetBool(IsHeadLeftRightAttack, false);
        //    return;
        //}
        //else
        //{
        //    headAnimator.SetBool(IsHeadUpAttack, false);
        //    headAnimator.SetBool(IsHeadDownAttack, false);
        //}
        //if (dir.x != 0)
        //{
        //    headAnimator.SetBool(IsHeadLeftRightAttack, true);
        //    if (dir.x >= 0)
        //    {
        //        sr.flipX = false;
        //    }
        //    else
        //    {
        //        sr.flipX = true;
        //    }
        //}
        //else
        //{
        //    headAnimator.SetBool(IsHeadLeftRightAttack, false);
        //}
    }

    private void AnimationMove(Vector2 dir)
    {
        AnimationAttackInit();
        if (dir.y > 0)
        {
            headAnimator.SetBool(IsHeadUp, true);
            headAnimator.SetBool(IsHeadMove, false);
            return;
        }
        else
        {
            headAnimator.SetBool(IsHeadUp, false);
        }

        if (dir.x != 0 && dir.y == 0)
        {
            headAnimator.SetBool(IsHeadMove, true);
            if (dir.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            headAnimator.SetBool(IsHeadMove, false);
        }
    }

    private void AnimationAttackInit()
    {
        headAnimator.SetBool(IsHeadUpAttack, false);
        headAnimator.SetBool(IsHeadDownAttack, false);
        headAnimator.SetBool(IsHeadLeftRightAttack, false);
    }
}
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
    protected float delay;
    protected bool canAttack;
    #endregion
    WaitForSeconds wait;
    protected virtual void Start()
    {
        canAttack = true;
        delay = 1f;
        wait = new WaitForSeconds(delay);
        StartCoroutine(AttackDelay());
    }

    protected abstract void Attack();

    IEnumerator AttackDelay()
    {
        while (true)
        {
            yield return new WaitWhile(() => canAttack);
            yield return wait;
            canAttack = true;
        }
    }
}
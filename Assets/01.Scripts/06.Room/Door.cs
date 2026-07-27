using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnCloseDoor()
    {
        animator.SetBool("isEnter", true);
    }
    public void OnOpneDoor()
    {
        animator.SetBool("isClear", true);
    }

}

using UnityEngine;

public class IsaacController : MonoBehaviour
{
    public StateMachine<IsaacController> stateMachine;
    
    void Start()
    {
        stateMachine = new StateMachine<IsaacController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

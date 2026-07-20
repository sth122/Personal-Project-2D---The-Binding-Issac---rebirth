using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void Initialize()
    {
        DontDestroyOnLoad(gameObject);
    }
}

using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    
    [field : SerializeField] public MonsterData MonsterData {  get; private set; }
    [field : SerializeField] public IsaacData IsaacData { get; private set; }
}

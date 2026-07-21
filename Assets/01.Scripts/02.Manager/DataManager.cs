using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    
    [field : SerializeField] public MonsterData MonsterData {  get; private set; }
}

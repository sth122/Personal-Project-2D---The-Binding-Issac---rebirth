using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct DukePattern
{
    public MonsterCurrentState pattern;
    public float patternTime;
}

[CreateAssetMenu(fileName = "DukePatternData", menuName = "Data/Duke")]
public class DukePatternData : ScriptableObject
{
    public List<DukePattern> patternList = new List<DukePattern>();
}

using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public struct DukePattern
{
    public BossPattern pattern;
    public float patternTime;
}

[CreateAssetMenu(fileName = "DukePatternData", menuName = "Data/Duke")]
public class DukePatternSO : ScriptableObject
{
    public List<DukePattern> patternList = new List<DukePattern>();
}

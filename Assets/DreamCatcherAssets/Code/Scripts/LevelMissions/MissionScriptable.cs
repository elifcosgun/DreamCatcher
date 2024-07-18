using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "MissionObject")]
public class MissionScriptable : ScriptableObject
{
    public MissionType missionType;
}
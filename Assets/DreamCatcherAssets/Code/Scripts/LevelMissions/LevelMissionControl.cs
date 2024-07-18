using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class LevelMissionControl : MonoBehaviour
{
    public List<GameObject> missions;

    private TextMeshPro _missionCount;
    void Start()
    {
        // ui update for missions
    }

    public void OnMissionObjectInteracted(GameObject mission)
    {
        missions.Remove(mission);
        // when you add the ui //missionCount.text = Missions.Count.ToString();
        //if (Missions == null)
        //{
        //    //all missions completed
        //}
    }
}

public enum MissionType
{
    CursedStatue,
    Nightmare
}

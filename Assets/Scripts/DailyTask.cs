using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MergeFactory/DailyTask")]
public class DailyTask : ScriptableObject
{
    public DailyTaskType dailyTaskType;
    public string taskName;
    public Sprite taskSprite;
    public float taskTarget;
}

public enum DailyTaskType
{
    DAILYLOGIN,
    BUYPLANTS,
    MERGEPLANTS,
    GETPLANTS,
    OPENMYSTERYBOX,
    WATCHVIDEO
}

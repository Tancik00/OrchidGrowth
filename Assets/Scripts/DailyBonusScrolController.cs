using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyBonusScrolController : MonoBehaviour
{
    public Transform scrollContent;
    public GameObject lastDayBonus;

    private void Update()
    {
        ControlScroll();
    }
    
    private void ControlScroll()
    {
        if (scrollContent.localPosition.x <= -9400f)
        {
            lastDayBonus.SetActive(false);
        }
        else
        {
            lastDayBonus.SetActive(true);
        }
    }
}

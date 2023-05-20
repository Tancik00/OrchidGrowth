using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;

public class GetPlantButtonOnMainMenu : MonoBehaviour
{
    public Image availablePlantImage;
    public GameObject priceObj;
    public GameObject getFreeObj;
    public Text priceText;
    public Text unitLvl;
    private Unit unit;
    private bool AdsEnabled;

    private void Awake()
    {
        AdsEnabled = true;
			
#if UNITY_EDITOR
        AdsEnabled = false;
#endif
    }

    private void Start()
    {
        if (DataProvider.instance.gameData.highestUnitUnlocked <= 3)
        {
            unit = UnitManager.instance.units[1];
        }
        else
        {
            unit = UnitManager.instance.units[DataProvider.instance.gameData.highestUnitUnlocked - 3];
        }
        availablePlantImage.sprite =
           unit.sprite;
        unitLvl.text = (unit.id + 1).ToString();
        if (DataProvider.instance.gameData.playerCoins >= unit.price)
        {
            priceObj.SetActive(true);
            getFreeObj.SetActive(false);
            priceText.text = unit.price.ToShortHandString();
        }
        else
        {
            getFreeObj.SetActive(true);
            priceObj.SetActive(false);
        }
    }

    private void Update()
    {
        if (DataProvider.instance.gameData.highestUnitUnlocked <= 3)
        {
            unit = UnitManager.instance.units[1];
        }
        else
        {
            unit = UnitManager.instance.units[DataProvider.instance.gameData.highestUnitUnlocked - 3];
        }
        availablePlantImage.sprite =
            unit.sprite;
        unitLvl.text = (unit.id + 1).ToString();
        if (DataProvider.instance.gameData.playerCoins >= unit.price)
        {
            priceObj.SetActive(true);
            getFreeObj.SetActive(false);
            priceText.text = unit.price.ToShortHandString();
        }
        else
        {
            getFreeObj.SetActive(true);
            priceObj.SetActive(false);
        }
    }

    public void WatchAdsToGetPlant()
    {
        if (AdsEnabled)
        {
            if (ads.instance.rewardedAvailable)
            {
                ads.instance.ShowRewardVideo("FreeItemOnMainMenu_REWARD");
                ads.instance.wasRequested = true;
                ads.instance.getFreeItemOnMainMenu = true;
            }
            else
            {
                GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
            }
        }
        else
        {
            GetPlantForFree();
        }
    }

    public void GetPlantForFree()
    {
        if (GridManager.instance.hasFreeSlot())
        {
            BoxManager.instance.CreateUnitBox(this.unit, true);
        }
        else
        {
            GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
            GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>()
                .ShowMessage("SORRY", "NOT ENOUGH LAND!", String.Empty, null, null, false);
        }
    }

    public void BuyPlant()
    {
        if (GridManager.instance.hasFreeSlot())
        {
            AchievementsManager.instance.IncrementAchievementEvent(AchievementType.BUY);
            BoxManager.instance.CreateUnitBox(this.unit, false);
            DataProvider.instance.gameData.playerCoins -= unit.price;
            unit.BuyUnit();
            SFXManager.instance.PlaySound(Sound.ItemPurchased);
            DailyTasksController.instance.IncrementDailyTaskEvent(DailyTaskType.BUYPLANTS);
        }
        else
        {
            GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
            GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>()
                .ShowMessage("SORRY", "NOT ENOUGH LAND!", String.Empty, null, null, false);
        }
    }
}

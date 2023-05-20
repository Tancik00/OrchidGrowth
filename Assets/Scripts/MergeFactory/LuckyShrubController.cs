using System;
using System.Collections;
using System.Collections.Generic;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Random1 = System.Random;

public class LuckyShrubController : MonoBehaviour
{
    private static LuckyShrubController _instance;

    public static LuckyShrubController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LuckyShrubController>();
            }

            return _instance;
        }
    }
    public List<Image> fortuneShrubs;
    public Button mergeBtn;
    public Button getLuckyShrubBtn;
    public Text revenueShare;
    public Text revenueShareOnMainMenu;
    public Text totalRevenue;

    public List<Sprite> luckyShrubsSprites;
    public List<string> luckyShrubsNames;
    
    public Color lockedColor;
    public PrizeWallController prizeWallController;
    
    private int openedFortuneShrubCount;
    private int _actualRevenue;

    private void Start()
    {
        _actualRevenue = prizeWallController.actualRevenue;
        revenueShare.text = "$" + _actualRevenue;
        revenueShareOnMainMenu.text = "$" + _actualRevenue;
        totalRevenue.text = "$" + _actualRevenue * 100 + ".00";
    }

    public void UpdateShrubPanel()
    {
        openedFortuneShrubCount = DataProvider.instance.gameData.openedFortuneShrubCount;
        UpdateShrubImage();
        
        prizeWallController.SetRewardListValues(10, prizeWallController.luckyShrubRewardListItemParent);
        /*if (openedFortuneShrubCount == fortuneShrubs.Count)
            mergeBtn.interactable = true;*/
    }

    private void UpdateShrubImage()
    {
        if (openedFortuneShrubCount >= 1)
            fortuneShrubs[0].color = Color.white;
        if (openedFortuneShrubCount >= 2)
            fortuneShrubs[1].color = Color.white;
        if (openedFortuneShrubCount >= 3)
            fortuneShrubs[2].color = Color.white;
        if (openedFortuneShrubCount >= 4)
            fortuneShrubs[3].color = Color.white;
    }

    public void MergeFortuneShrubs()
    {
        getLuckyShrubBtn.interactable = true;
    }

    public void GetLuckyShrub()
    {
        DataProvider.instance.gameData.openedFortuneShrubCount = 0;
        for (int i = 0; i < fortuneShrubs.Count; i++)
        {
            fortuneShrubs[i].color = lockedColor;
        }

        mergeBtn.interactable = false;
        getLuckyShrubBtn.interactable = false;
    }

    public void SetPanelMessageValues()
    { 
        openedFortuneShrubCount = DataProvider.instance.gameData.openedFortuneShrubCount;
        switch (openedFortuneShrubCount)
        {
            case 1:
                GUIManager.Instance.CURRENTPANEL.GetComponent<NewUnitMessage>().ShoeMessageForGettingLuckyShrub(luckyShrubsSprites[openedFortuneShrubCount-1], luckyShrubsNames[openedFortuneShrubCount-1]);
                break;
            case 2:
                GUIManager.Instance.CURRENTPANEL.GetComponent<NewUnitMessage>().ShoeMessageForGettingLuckyShrub(luckyShrubsSprites[openedFortuneShrubCount-1], luckyShrubsNames[openedFortuneShrubCount-1]);
                break;
            case 3:
                GUIManager.Instance.CURRENTPANEL.GetComponent<NewUnitMessage>().ShoeMessageForGettingLuckyShrub(luckyShrubsSprites[openedFortuneShrubCount-1], luckyShrubsNames[openedFortuneShrubCount-1]);
                break;
            case 4:
                GUIManager.Instance.CURRENTPANEL.GetComponent<NewUnitMessage>().ShoeMessageForGettingLuckyShrub(luckyShrubsSprites[openedFortuneShrubCount-1], luckyShrubsNames[openedFortuneShrubCount-1]);
                break;
            default:
                var randomShrub = Random.Range(0, luckyShrubsSprites.Count);
                GUIManager.Instance.CURRENTPANEL.GetComponent<NewUnitMessage>().ShoeMessageForGettingLuckyShrub(luckyShrubsSprites[randomShrub], luckyShrubsNames[randomShrub]);
                break;
        }
    }
}

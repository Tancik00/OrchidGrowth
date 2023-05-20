using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlyingPuzzle : MonoBehaviour
{
   private static FlyingPuzzle _instance;

   public static FlyingPuzzle Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<FlyingPuzzle>();
         }

         return _instance;
      }
   }

   public GameObject mysteryBubblePrizePanel;
   public GameObject noThanksButton;
   public GameObject gettingBubblePrizePanel;
   public Image gettingPrizeImage;
   public Text gettingPrizeText;
   public Transform wayPoints;
   public GameObject bubblePref;
   public GameObject gridBubblePref;
   
   [Header("Flying Bubble Prizes")]
   public GameObject flyingBubblePrizePanel;
   public GameObject flyingBubblePanelNoThanksButton;
   public Image flyingBubblePrizeImage;
   public Text flyingBubblePrizeName;
   public Text flyingBubblePrizeDescription;

   [Header("Active Option Timer")] 
   public GameObject mainScreenActiveOptionTimer;
   public Text activeOptionTimerText;
   public Image activeOptionImage;

   public List<Transform> points;
   
   //[HideInInspector] public Sprite prizeSprite;
   [HideInInspector] public int prizeID;

   [HideInInspector] public Sprite flyingPrizeSprite;
   [HideInInspector] public int flyingPrizeID;

   [HideInInspector] public float timeInSec = 90f; 
   [HideInInspector] public float timeInSecForGridBubble = 180f;

   public LuckyPrizesManager luckyPrizesManager;
   public Prizes[] prizeData;
   public PrizeParameters[] prizeParameters;

   private GameObject _bubblePuzzle;
   private GameObject _gridBubblePuzzle;

   private float _instantCoinsToBuy;

   private bool _isOptionActive;
   private float _activeOptionTimer = 90f;

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
      prizeData = luckyPrizesManager.prizeDatabase;
      for (int i = 0; i < wayPoints.childCount; i++)
      {
         points.Add(wayPoints.GetChild(i));
      }

      timeInSec = 90f;
      timeInSecForGridBubble = 180f;

      _instantCoinsToBuy = (UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f)/5f;

      ResetFlyingBubbleOptions();
   }

   private void Update()
   {
      if (timeInSec > 0f)
      {
         timeInSec -= Time.deltaTime;
         if (timeInSec < 0)
         {
            _bubblePuzzle = Instantiate(bubblePref, wayPoints.GetChild(0));
         }
      }

      if (timeInSecForGridBubble > 0f)
      {
         timeInSecForGridBubble -= Time.deltaTime;
         if (timeInSecForGridBubble < 0)
         {
            if (!GridManager.instance.hasFreeSlot())
            {
               timeInSecForGridBubble = 180f;
            }
            else
            {
               Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
               _gridBubblePuzzle = Instantiate(gridBubblePref, randomFreeSlot.transform);
            }
         }
      }

      if (_isOptionActive)
      {
         _activeOptionTimer -= Time.deltaTime;
         
         float minutes = Mathf.FloorToInt(_activeOptionTimer / 60);  
         float seconds = Mathf.FloorToInt(_activeOptionTimer % 60);
         activeOptionTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
         if (_activeOptionTimer <= 0f)
         {
            ResetFlyingBubbleOptions();
         }
      }
   }

   private void ResetFlyingBubbleOptions()
   {
      DataProvider.instance.gameData.boxIAPMultiplier = 1; 
      DataProvider.instance.gameData.giftProbMultiplier = 1;
      mainScreenActiveOptionTimer.SetActive(false);
      _activeOptionTimer = 90f;
      _isOptionActive = false;
   }

   public void SetBubblePuzzleValues()
   {
      var prizeIndex = Random.Range(0, prizeData.Length);
      for (int i = 0; i < prizeData.Length; i++)
      {
         //prizeSprite = prizeData[prizeIndex].sprite;
         prizeID = prizeData[prizeIndex].ID;
      }
   }

   public void SetFlyingBubbleValues()
   {
      var prizeIndex = Random.Range(0, prizeParameters.Length);
      flyingPrizeID = prizeIndex;
      flyingPrizeSprite = prizeParameters[prizeIndex].prizeSprite;
      
      activeOptionImage.sprite = prizeParameters[prizeIndex].prizeSprite;
   }

   public void AddPrizePoints(int prizeID)
   {
      luckyPrizesManager.AddPoints(prizeID);
   }

   public void WatchAdsForBubblePrize()
   {
      if (AdsEnabled)
      {
         if (ads.instance.rewardedAvailable)
         {
            ads.instance.ShowRewardVideo("BubblePuzzle_REWARDED");
            ads.instance.wasRequested = true;
            ads.instance.bubblePuzzle = true;
         }
         else
         {
            GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
            GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE",
               "TRY AGAIN LATER !", string.Empty, null, null, false);
         }
      }
      else
      {
         GetBubblePrize();
      }
   }

   public void GetBubblePrize()
   {
      AddPrizePoints(prizeID);
      gettingBubblePrizePanel.SetActive(true);
      gettingPrizeImage.sprite = prizeData[prizeID].sprite;
      gettingPrizeText.text = prizeData[prizeID].name + " puzzle";
      FlyingPuzzle.Instance.timeInSecForGridBubble = 180f;
   }

   public void WatchAdsForFlyingBubblePrize()
   {
      if (AdsEnabled)
      {
         if (ads.instance.rewardedAvailable)
         {
            ads.instance.ShowRewardVideo("FlyingBubblePuzzle_REWARDED");
            ads.instance.wasRequested = true;
            ads.instance.flyingBubblePuzzle = true;
         }
         else
         {
            GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
            GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE",
               "TRY AGAIN LATER !", string.Empty, null, null, false);
         }
      }
      else
      {
         GetFlyingBubblePrize();
      }
   }
   
   public void GetFlyingBubblePrize()
   {
      switch (flyingPrizeID)
      {
         case 0: 
            DataProvider.instance.gameData.boxIAPMultiplier = 2;
            _isOptionActive = true;
            mainScreenActiveOptionTimer.SetActive(true);
            break;
         case 1: 
            DataProvider.instance.gameData.giftProbMultiplier = 2;
            _isOptionActive = true;
            mainScreenActiveOptionTimer.SetActive(true);
            break;
         case 2: 
            Debug.Log("=== instant coins: " + UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f);
            _instantCoinsToBuy = (UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f)/5f; 
            DataProvider.instance.gameData.playerCoins += _instantCoinsToBuy;
            break;
      }
      
      timeInSec = 90f;
   }

   public void ShowNoThanksButton()
   {
      StartCoroutine(ShowNoThanksButtonCoroutine());
   }

   public void ShowFlyingBubblePanelNoThanksButton()
   {
      flyingBubblePrizeImage.sprite = prizeParameters[flyingPrizeID].prizeSprite;
      flyingBubblePrizeName.text = prizeParameters[flyingPrizeID].prizeName;
      flyingBubblePrizeDescription.text = prizeParameters[flyingPrizeID].prizeDescription;
      if (flyingPrizeID == 2)
      {
         flyingBubblePrizeDescription.text = "Claim " + _instantCoinsToBuy.ToShortHandString() + " Coins for Free";
      }
      StartCoroutine(ShowFlyingBubbleNoThanksButtonCoroutine());
   }

   private IEnumerator ShowNoThanksButtonCoroutine()
   {
      yield return new WaitForSeconds(1f);
      noThanksButton.SetActive(true);
   }

   private IEnumerator ShowFlyingBubbleNoThanksButtonCoroutine()
   {
      yield return new WaitForSeconds(1f);
      flyingBubblePanelNoThanksButton.SetActive(true);
   }

   public void ClickNoThanksButton()
   {
      timeInSecForGridBubble = 180f;
   }

   public void ClickFlyingBubbleNoThanksButton()
   {
      timeInSec = 90f;
   }
}

[Serializable]
public class PrizeParameters
{
   public int ID;
   public Sprite prizeSprite;
   public string prizeName;
   public string prizeDescription;
}

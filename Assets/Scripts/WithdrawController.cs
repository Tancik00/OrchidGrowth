using System;
using System.Collections;
using MergeFactory;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WithdrawController : MonoBehaviour
{
   private static WithdrawController _instance;
   public static WithdrawController Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<WithdrawController>();
         }

         return _instance;
      }
   }

   public PrizeWallController prizeWallController;
   public Text cashTxt;
   public Text cashTxtOnMainScreen;
   public Text BTCText;
   public Text OTKText;
   public Text OTKInPanelText;
   
   public Transform winnerNotificationParent;
   public GameObject winnerNotification;
   public GameObject withdrawRulesPanel;
   [HideInInspector] public float _timeInSec;
   
   private void Start()
   {
      cashTxtOnMainScreen.text = DataProvider.instance.gameData.myCash.ToString("F2");
      GetRandomTime();
   }

   private void Update()
   {
      if (_timeInSec > 0f)
      {
         _timeInSec -= Time.deltaTime;
         if (_timeInSec < 0)
         {
            Instantiate(winnerNotification, winnerNotificationParent);
         }
      }
   }

   public void UpdateCashText()
   {
      cashTxt.text = DataProvider.instance.gameData.myCash.ToString("F2") + "<size=10>$</size>";
      BTCText.text = DataProvider.instance.gameData.BTC.ToString();
      OTKText.text = DataProvider.instance.gameData.orchidToken.ToString();
      OTKInPanelText.text = DataProvider.instance.gameData.orchidToken + " OTK";
      
      prizeWallController.SetCashRewardListValues(10, prizeWallController.cashRewardListItemParent);
   }

   public void UpdateCashTextOnMainScreen()
   {
      cashTxtOnMainScreen.text = DataProvider.instance.gameData.myCash.ToString("F2");
   }

   public void GetRandomTime()
   {
      _timeInSec = Random.Range(30, 120);
   }

   public void Transfer()
   {
      GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
      GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("", "YOU NEED TO COLLECT THE MINIMUM AMOUNT", String.Empty, null, null, false);
   }
}

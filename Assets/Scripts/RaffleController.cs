using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaffleController : MonoBehaviour
{
   private static RaffleController _instance;

   public static RaffleController Instance
   {
      get
      {
         if (_instance == null)
         {
            _instance = FindObjectOfType<RaffleController>();
         }

         return _instance;
      }
   }

   public int raffleNumber
   {
      get
      {
         return PlayerPrefs.GetInt("raffleNumber", 1001);
      }
      set
      {
         PlayerPrefs.SetInt("raffleNumber", value);
      }
   }
   public Text raffleNumberText;
   public PrizeWallController prizeWallController;

   private void Start()
   {
      raffleNumber = 1000 + (PlayerPrefs.GetInt("totalHistoryItem") + 1);
   }
   
   public void UpdateRaffleInfo()
   {
      raffleNumberText.text = raffleNumber.ToString();
      prizeWallController.InstantiatePrizeWallItem();
   }
}

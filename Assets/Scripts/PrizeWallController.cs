using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = System.Random;

public class PrizeWallController : MonoBehaviour
{
   public Transform luckyShrubRewardList;
   public Transform cashRewardList;
   public Transform freeRaffleRewardList;
   public GameObject luckyShrubButton;
   public GameObject cashButton;
   public GameObject freeRaffleButton;
   public Color selectedColor;
   public Color unselectedColor;
   public GameObject rewardListItemPref;
   public GameObject rafflePrizeWallitemPref;
   public Transform luckyShrubRewardListItemParent;
   public Transform cashRewardListItemParent;
   public Transform freeRaffleRewardListItemParent;
   public GameObject topList;
   public List<string> prizeWallTexts;
   public List<Sprite> prizeWallImages;
   public List<int> IDs;
   [HideInInspector] public int actualRevenue;
   private int _seed;
   private string savedDate;

   public int prizeWallItemCount
   {
      get { return PlayerPrefs.GetInt("prizeWallItemCount", 0); }
      set { PlayerPrefs.SetInt("prizeWallItemCount", value); }
   }

   private void Awake()
   {
      _seed = DateTime.UtcNow.Day;
      Random rng = new Random(_seed);
      actualRevenue = rng.Next(160, 200);
      PlayerPrefs.SetString(DateTime.Today.ToString(), actualRevenue.ToString());
      GenerateIDs();

      CountDaysForFreeRafflePrizeItemAppearance();

      if (CountDaysForFreeRafflePrizeItemAppearance() >= 7)
      {
         prizeWallItemCount++;
         PlayerPrefs.SetString("daysForRafflePrizeItemAppearance", String.Empty);
      }
   }

   private int CountDaysForFreeRafflePrizeItemAppearance()
   {
      if (PlayerPrefs.GetString("daysForRafflePrizeItemAppearance") == String.Empty)
      {
         PlayerPrefs.SetString("daysForRafflePrizeItemAppearance", DateTime.Now.ToString());
         PlayerPrefs.Save();
      }

      return DateTime.Now.Subtract(DateTime.Parse(PlayerPrefs.GetString("daysForRafflePrizeItemAppearance"))).Days;
   }

   private bool _isEnteredOnMethod;

   public void ChooseCategory(int buttonID)
   {
      switch (buttonID)
      {
         case 0:
            ChangeCategory(luckyShrubButton, cashButton, freeRaffleButton, luckyShrubRewardList.gameObject,
               cashRewardList.gameObject, freeRaffleRewardList.gameObject);
            SetRewardListValues(100, luckyShrubRewardList);
            topList.SetActive(true);
            break;
         case 1:
            ChangeCategory(cashButton, luckyShrubButton, freeRaffleButton, cashRewardList.gameObject,
               luckyShrubRewardList.gameObject, freeRaffleRewardList.gameObject);
            SetCashRewardListValues(100, cashRewardList);
            topList.SetActive(true);
            break;
         case 2:
            ChangeCategory(freeRaffleButton, luckyShrubButton, cashButton, freeRaffleRewardList.gameObject,
               luckyShrubRewardList.gameObject, cashRewardList.gameObject);
            SetPrizeWallValues();
            topList.SetActive(false);
            break;
      }
   }

   public void InstantiatePrizeWallItem()
   {
      var prizeWallItem = Instantiate(rafflePrizeWallitemPref, freeRaffleRewardListItemParent);
      var rewardListItemScript = prizeWallItem.GetComponent<RewardListItem>();

      GeneratePrizeWall(prizeWallItemCount, rewardListItemScript, -CountDaysForFreeRafflePrizeItemAppearance());
   }

   private void SetPrizeWallValues()
   {
      var day = prizeWallItemCount;
      int index = 0;

      if (freeRaffleRewardList.childCount > 12)
      {
         for (int j = 0; j < freeRaffleRewardList.childCount-(freeRaffleRewardList.childCount-(prizeWallItemCount+1)); j++)
         {
            Destroy(freeRaffleRewardList.GetChild(j).gameObject);
         }
      }

      for (int i = 0; i < prizeWallItemCount + 1; i++)
      {
         var siblingIndex = 0;
         var prizeWallItem = Instantiate(rafflePrizeWallitemPref, freeRaffleRewardList);
         prizeWallItem.transform.SetSiblingIndex(siblingIndex);
         var rewardListItemScript = prizeWallItem.GetComponent<RewardListItem>();
         
         GeneratePrizeWall(index, rewardListItemScript, (-CountDaysForFreeRafflePrizeItemAppearance() + (day * (-7))));
         day--;
         siblingIndex++;
         index++;
         if (index >= 14)
         {
            index = 0;
         }
      }
   }

   private void GeneratePrizeWall(int index, RewardListItem rewardListItemScript, int day)
   {
      rewardListItemScript.idText.text = IDs[index].ToString().Substring(2, 3) + "****" +
                                         IDs[index].ToString().Substring(0, 2) + IDs[index].ToString().Substring(5);

      rewardListItemScript.shareText.text = prizeWallTexts[index];

      if (rewardListItemScript.itemImage != null)
      {
         rewardListItemScript.itemImage.sprite = prizeWallImages[index];
      }

      rewardListItemScript.dateText.text = DateTime.UtcNow.Date.AddDays(day).Day + "/" +
                                           DateTime.UtcNow.Date.AddDays(day).Month + "/" +
                                           DateTime.UtcNow.Date.AddDays(day).Year;
   }

   private void ChangeCategory(GameObject pressedutton, GameObject unpressedutton1, GameObject unpressedutton2,
      GameObject selectedList, GameObject unselectedList1, GameObject unselectedList2)
   {
      pressedutton.GetComponent<Text>().color = selectedColor;
      pressedutton.transform.GetChild(0).gameObject.SetActive(true);
      selectedList.SetActive(true);
      unpressedutton1.GetComponent<Text>().color = unselectedColor;
      unpressedutton1.transform.GetChild(0).gameObject.SetActive(false);
      unselectedList1.SetActive(false);
      unpressedutton2.GetComponent<Text>().color = unselectedColor;
      unpressedutton2.transform.GetChild(0).gameObject.SetActive(false);
      unselectedList2.SetActive(false);
   }

   private void GenerateIDs()
   {
      Random rng = new Random(_seed);
      for (int i = 0; i < 100; i++)
      {
         IDs.Add(rng.Next(100000, 1000000));
      }
   }

   public void SetRewardListValues(int count, Transform parent)
   {
      for (int i = 0; i < count; i++)
      {
         var rewardListItem = Instantiate(rewardListItemPref, parent);
         var rewardListItemScript = rewardListItem.GetComponent<RewardListItem>();
         rewardListItemScript.idText.text = IDs[i].ToString().Substring(0, 3) + "****" + IDs[i].ToString().Substring(3);
         if (rewardListItemScript.amountText != null)
         {
            rewardListItemScript.amountText.text = "1";
         }
         
         SetDateValues(rewardListItemScript, i);
      }
   }
   
   public void SetCashRewardListValues(int count, Transform parent)
   {
      for (int i = 0; i < count; i++)
      {
         var rewardListItem = Instantiate(rewardListItemPref, parent);
         var rewardListItemScript = rewardListItem.GetComponent<RewardListItem>();
         rewardListItemScript.idText.text = IDs[i].ToString().Substring(3) + "****" + IDs[i].ToString().Substring(0, 3);
         if (rewardListItemScript.amountText != null)
         {
            rewardListItemScript.amountText.text = "1";
         }

         SetDateValues(rewardListItemScript, i);
         rewardListItemScript.shareText.text = "$150";
         
         if ((i % 10 == 0 || i % 30 == 0) && i != 0)
         {
            rewardListItemScript.shareText.text = "0.1 BTC";
         }
      }
   }

   private void SetDateValues(RewardListItem rewardListItem, int count)
   {
      if (count >= 0)
      {
         rewardListItem.dateText.text = DateTime.UtcNow.Date.Day + "/" + DateTime.UtcNow.Date.Month + "/" +
                                        DateTime.UtcNow.Date.Year;
         rewardListItem.shareText.text = "$" + actualRevenue;
      }

      if (count > 15)
      {
         SetValues(rewardListItem, -1);
      }

      if (count > 30)
      {
         SetValues(rewardListItem, -2);
      }

      if (count > 50)
      {
         SetValues(rewardListItem, -3);
      }

      if (count > 70)
      {
         SetValues(rewardListItem, -4);
      }
   }

   private void SetValues(RewardListItem rewardListItem, int day)
   {
      rewardListItem.dateText.text = DateTime.UtcNow.Date.AddDays(day).Day + "/" +
                                     DateTime.UtcNow.Date.AddDays(day).Month + "/" +
                                     DateTime.UtcNow.Date.AddDays(day).Year;
      if (PlayerPrefs.GetString(DateTime.Today.AddDays(day).ToString()) == String.Empty)
         rewardListItem.shareText.text = "$170";
      else
         rewardListItem.shareText.text = "$" + PlayerPrefs.GetString(DateTime.Today.AddDays(day).ToString());
   }

   public void DestroyRewardListClones()
   {
      for (int i = 0; i < 10; i++)
      {
         if (luckyShrubRewardListItemParent.childCount != 0)
            Destroy(luckyShrubRewardListItemParent.GetChild(i).gameObject);
         if (cashRewardListItemParent.childCount != 0)
            Destroy(cashRewardListItemParent.GetChild(i).gameObject);
      }

      for (int j = 0; j < 100; j++)
      {
         if (luckyShrubRewardList.childCount != 0)
            Destroy(luckyShrubRewardList.GetChild(j).gameObject);
         if (cashRewardList.childCount != 0)
            Destroy(cashRewardList.GetChild(j).gameObject);
      }

      for (int z = 0; z < prizeWallItemCount; z++)
      {
         Destroy(freeRaffleRewardList.GetChild(z).gameObject);
      }
   }
}
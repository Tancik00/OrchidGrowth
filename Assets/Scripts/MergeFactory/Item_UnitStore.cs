using Mindravel.UI;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class Item_UnitStore : MonoBehaviour
	{
		public Unit unit;

		[Header("UI Elements")]
		public GameObject item_ImageBG;

		public GameObject default_Image;

		public Image item_Image;

		public Text itemName_Text;

		public Text price_Text;

		[Space(5f)]
		public GameObject locked_Button;

		public GameObject buy_Button;

		public GameObject buyText;

		public GameObject free_Button;

		public GameObject new_Item;
		public GameObject unlockButton;

		public Sprite[] buySprites;

		private bool canBuyFromVideo;

		private bool AdsEnabled;

        private void Awake()
        {
			AdsEnabled = true;
			
#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}

        //private static MRUtilities.RewardedVideoAction __f__am_cache0;

        public void SetItem(bool unlocked, bool _canBuyFromVideo = false, bool newItem = false)
		{
			this.canBuyFromVideo = _canBuyFromVideo;
			if (unlocked)
			{
				this.new_Item.SetActive(newItem);
				this.item_Image.sprite = UnitStore.instance.storeItemSprites[this.unit.id];
				this.item_ImageBG.SetActive(true);
				this.default_Image.SetActive(false);
				this.itemName_Text.text = this.unit.name;
				this.locked_Button.SetActive(false);
				this.price_Text.text = this.unit.price.ToShortHandString();
				if (this.canBuyFromVideo)
				{
					this.free_Button.SetActive(true);
				}
				else
				{
					this.free_Button.SetActive(false);
				}
				this.buy_Button.SetActive(true);
			}
			else
			{
				this.new_Item.SetActive(false);
				this.itemName_Text.text = "????";
				this.item_ImageBG.SetActive(false);
				this.default_Image.SetActive(true);
				this.locked_Button.SetActive(true);
				this.buy_Button.SetActive(false);
				this.free_Button.SetActive(false);
			}
		}

		public void BuyItem_Unit()
		{
			UnityEngine.Debug.Log(base.gameObject.name);
			UnityEngine.Debug.Log(this.unit.id + " - " + this.unit.name);
			if (this.unit.price <= DataProvider.instance.gameData.playerCoins)
			{
				if (GridManager.instance.hasFreeSlot())
				{
                    //MRUtilities.Instance.LogEvent("ItemPurchased", "ItemID", this.unit.id.ToString());
                    //FireBaseManager.Instance.LogScreen("ItemPurchased");
                    AchievementsManager.instance.IncrementAchievementEvent(AchievementType.BUY);
					BoxManager.instance.CreateUnitBox(this.unit, false);
					DataProvider.instance.gameData.playerCoins -= this.unit.price;
					this.unit.BuyUnit();
					this.SetItem(true, this.canBuyFromVideo, false);
					SFXManager.instance.PlaySound(Sound.ItemPurchased);
					DailyTasksController.instance.IncrementDailyTaskEvent(DailyTaskType.BUYPLANTS);
				}
				else
				{
					GUIManager.Instance.Back();
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("SORRY", "NOT ENOUGH LAND!", string.Empty, null, null, false);
				}
			}
			else
			{
				SFXManager.instance.PlaySound(11);
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.CoinsShop);
			}
		}
		
		public void FreeItem_Unit()
		{
			SFXManager.instance.PlaySound(Sound.ButtonTap);

			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.itemUnitScript = this;
					ads.instance.ShowRewardVideo("FreeItem_REWARDED");	
				}
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				}
			}
			else
				FreeItemReward();

		}

		public void FreeItemReward()
        {
			if (GridManager.instance.hasFreeSlot())
			{
				//MRUtilities.Instance.LogEvent("VideoBoxButtonClicked");
				//FireBaseManager.Instance.LogScreen("VideoBoxButtonClicked");
				//print("FREE ITEM BITCH!");
				BoxManager.instance.CreateUnitBox(this.unit, true);
				this.SetItem(true, this.canBuyFromVideo, false);

				//if (AdsControl.Instance.GetRewardAvailable())
				//{
				//    AdsControl.Instance.PlayDelegateRewardVideo(delegate
				//    {
				//        BoxManager.instance.CreateUnitBox(this.unit, true);
				//        this.SetItem(true, this.canBuyFromVideo, false);
				//        // MRUtilities.Instance.LogEvent("VideoBoxEarned");
				//        //FireBaseManager.Instance.LogScreen("VideoBoxEarned");
				//    });
				//}
				//else
				//{
				//    GUIManager.Instance.Back();
				//    GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				//    GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("SORRY", "NOT ENOUGH LAND!", string.Empty, null, null, false);
				//    // MRUtilities.Instance.LogEvent("LandNotAvailable");
				//    //FireBaseManager.Instance.LogScreen("LandNotAvailable");
				//}

				/*
                MRUtilities.Instance.ShowVideoAd(delegate
				{
					BoxManager.instance.CreateUnitBox(this.unit, true);
					this.SetItem(true, this.canBuyFromVideo, false);
					MRUtilities.Instance.LogEvent("VideoBoxEarned");
				}, delegate
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
					MRUtilities.Instance.LogEvent("VideoNotAvailableVideoBox");
				});
				*/
			}
			else
			{
				GUIManager.Instance.Back();
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("SORRY", "NOT ENOUGH LAND!", string.Empty, null, null, false);
				//MRUtilities.Instance.LogEvent("LandNotAvailable");
				//FireBaseManager.Instance.LogScreen("LandNotAvailable");
			}
		}

		private void Update()
		{
			if (this.buy_Button.activeInHierarchy)
			{
				if (this.unit.price <= DataProvider.instance.gameData.playerCoins)
				{
					this.buy_Button.GetComponent<Image>().sprite = this.buySprites[0];
					this.buyText.SetActive(true);
				}
				else
				{
					this.buy_Button.GetComponent<Image>().sprite = this.buySprites[1];
					this.buyText.SetActive(false);
				}
			}
		}
	}
}

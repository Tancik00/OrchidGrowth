using Mindravel.UI;
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MergeFactory
{
	public class ShopManager : MonoBehaviour
	{
		public static ShopManager instance;

		public RectTransform shopPanel;
		public GameObject removeAdsButton;

		public GameObject shopButton;

		public GameObject starterPackButton;

		public GameObject doubleCoinButton;

		public GameObject landFillButton;

		[Header("Shop Items")] public GameObject removeAds;

		public GameObject moreCoins;

		public GameObject betterCrates;

		public GameObject fasterCrates;

		public GameObject moreMystry;

		public GameObject autoFillWatch;

		public GameObject autoFill100;

		public GameObject autoFill300;

		public GameObject autoFill1000;

		[Header("Shop Item Disc")] public Text betterCrate_Disc;

		public Text betterCrate_Price;
		public Text fasterCratePrice;
		public Text moreMystryPrice;
		public Text buyCoinsPrice;
		public Text buyMegaCoinsPrice;
		public Text buyCoinsPrice_CoinsPanel;
		public Text buyMegaCoinsPrice_CoinsPanel;
		public Text landFill100Price;
		public Text landFill300Price;
		public Text landFill1000Price;
		public Text landFill100Price_AutoFillPanel;
		public Text landFill300Price_AutoFillPanel;
		public Text landFill1000Price_AutoFillPanel;
		public Text starterPackPrice;
		public Text megaPackPrice;
		public Text coin_Disc;

		public Text megaCoin_Disc;

		[Header("StarterPack Disc")] public Text boxLevel_SP;

		public Text bonusCoin_SP;

		public Text bonusCoin_MP;

		public Image crateImage;

		public Sprite[] crateSprites;

		[Header("IAP banners")] 
		public GameObject fasterCrate_Banner;
		public GameObject moreMystry_Banner;

		private float instantCoinsToBuy;

		private float megaCoinsToBuy;
		private bool AdsEnabled;
		private Vector3 _shopPanelInitPosition;

		private bool FIRSTADIMPRESSIONSHOWN
		{
			get { return PlayerPrefs.HasKey("firstAdImprssionsShown"); }
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("firstAdImprssionsShown", 1);
				}
				else
				{
					PlayerPrefs.DeleteKey("firstAdImprssionsShown");
				}
			}
		}

		public bool firstTime_StarterPackSHOWN
		{
			get { return PlayerPrefs.HasKey("firstTime_StarterPackSHOWN"); }
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("firstTime_StarterPackSHOWN", 1);
				}
				else
				{
					PlayerPrefs.DeleteKey("firstTime_StarterPackSHOWN");
				}
			}
		}

		private void Awake()
		{
			ShopManager.instance = this;

			AdsEnabled = true;

#if UNITY_EDITOR
        AdsEnabled = false;
#endif
		}

		private void Start()
		{
			UpdateShop();
			_shopPanelInitPosition = shopPanel.anchoredPosition;
		}

		public void ShowInterstitialAd()
		{
			//if (DataProvider.instance.gameData.removeAds == 1 || PlayerPrefs.GetInt("RemoveAds") == 1)
			//{
				if (!this.FIRSTADIMPRESSIONSHOWN)
				{
					this.FIRSTADIMPRESSIONSHOWN = true;
				}
				else
				{
					//MRUtilities.Instance.ShowInterstitialAd(true);
					//AdsControl.Instance.showAds();
					ads.instance.ShowInterestial("FirstAd_INTER");
					print("Show AD");
				}
			//}

			UpdateShop();
		}

		public void UpdateStarterPackUI()
		{
			if (((LevelManager.instance.currentLevel.levelNo == 5 && DataProvider.instance.gameData.playerLevelValue >
				    LevelManager.instance.currentLevel.MaxRange / 2) ||
			    LevelManager.instance.currentLevel.levelNo > 5) &&
			    !StarterPackManager.instance.StarterPackBought)
			{
				if (DataProvider.instance.gameData.currentBoxLevel < 2)
				{
					this.boxLevel_SP.text = string.Concat(new object[]
					{
						"Level ",
						DataProvider.instance.gameData.currentBoxLevel + 1,
						" --> ",
						DataProvider.instance.gameData.currentBoxLevel + 3
					});
				}
				else
				{
					this.boxLevel_SP.text = "Level " + (DataProvider.instance.gameData.currentBoxLevel + 1) + " --> 5";
				}

				this.bonusCoin_SP.text = (DataProvider.instance.gameData.playerCoins * 3f).ToShortHandString();
				this.bonusCoin_MP.text = (DataProvider.instance.gameData.playerCoins * 6f).ToShortHandString();
				/*if (this.firstTime_StarterPackSHOWN)
				{
					this.starterPackButton.SetActive(true);
				}
				else
				{
					this.firstTime_StarterPackSHOWN = true;
					StarterPackManager.instance.SaveStarterPackEndTime();
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.StarterPack);
				}

				StarterPackManager.instance.UpdateStarterPack();*/
			}
			else
			{
				this.starterPackButton.SetActive(false);
			}

			this.starterPackPrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.starterPack, 0) + "/39";
			this.megaPackPrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.megaPack, 0) + "/99";
		}

		public void UpdateShop()
		{
			UnityEngine.Debug.Log(LevelManager.instance.currentLevelIndex);
			if (LevelManager.instance.currentLevelIndex >= 9)
			{
				this.autoFillWatch.SetActive(true);
				this.autoFill100.SetActive(true);
				this.autoFill300.SetActive(true);
				this.autoFill1000.SetActive(true);
			}
			else
			{
				this.autoFillWatch.SetActive(true);
				this.autoFill100.SetActive(true);
				this.autoFill300.SetActive(true);
				this.autoFill1000.SetActive(true);
			}

			if (!this.FIRSTADIMPRESSIONSHOWN)
			{
				this.removeAdsButton.SetActive(false);
			}
			/*else if (LevelManager.instance.currentLevel.levelNo < 5 && DataProvider.instance.gameData.removeAds == 0)
			{
				this.removeAdsButton.SetActive(true);
			}*/
			else
			{
				this.removeAdsButton.SetActive(false);
			}

			if (LevelManager.instance.currentLevel.levelNo > 9)
			{
				this.landFillButton.SetActive(true);
			}
			else
			{
				this.landFillButton.SetActive(true);
			}

			//this.UpdateStarterPackUI();
			if (TutorialManager.instance.TUTCompleted)
			{
				this.doubleCoinButton.SetActive(true);
			}
			else
			{
				this.doubleCoinButton.SetActive(false);
			}

			/*if (LevelManager.instance.currentLevel.levelNo >= 5)
			{
				this.shopButton.SetActive(true);
			}
			else
			{
				this.shopButton.SetActive(true);
			}*/
			
			this.betterCrate_Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.betterCrates) + "/9";
			this.fasterCratePrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.fasterCrates) + "/19";
			this.moreMystryPrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.moreMystry) + "/19";
			this.buyCoinsPrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyCoins) + "/9";
			this.buyCoinsPrice_CoinsPanel.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyCoins) + "/9";
			this.buyMegaCoinsPrice.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyMegaCoins) + "/49";
			this.buyMegaCoinsPrice_CoinsPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyMegaCoins) + "/49";
			this.landFill100Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 100) + "/9";
			this.landFill300Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 300) + "/19";
			this.landFill1000Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 1000) + "/49";
			this.landFill100Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 100) + "/9";
			this.landFill300Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 300) + "/19";
			this.landFill1000Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 1000) + "/49";
			
			if (DataProvider.instance.gameData.currentBoxLevel < 4)
			{
				this.crateImage.sprite = this.crateSprites[DataProvider.instance.gameData.currentBoxLevel + 1];
				/*if (DataProvider.instance.gameData.currentBoxLevel == 0)
				{
					this.betterCrate_Price.text = "$ 0.99";
				}
				else if (DataProvider.instance.gameData.currentBoxLevel == 1)
				{
					this.betterCrate_Price.text = "$ 1.99";
				}
				else if (DataProvider.instance.gameData.currentBoxLevel == 2)
				{
					this.betterCrate_Price.text = "$ 2.99";
				}
				else if (DataProvider.instance.gameData.currentBoxLevel == 3)
				{
					this.betterCrate_Price.text = "$ 3.99";
				}*/
				this.betterCrate_Disc.text = string.Concat(new object[]
				{
					"Level ",
					DataProvider.instance.gameData.currentBoxLevel + 1,
					" --> ",
					DataProvider.instance.gameData.currentBoxLevel + 2
				});
				this.betterCrates.SetActive(true);
			}
			else
			{
				this.betterCrates.SetActive(false);
			}

			/*if (DataProvider.instance.gameData.boxIAPMultiplier == 1)
			{
				this.fasterCrates.SetActive(true);
				this.fasterCrate_Banner.SetActive(false);
			}
			else
			{
				this.fasterCrates.SetActive(false);
				this.fasterCrate_Banner.SetActive(true);
			}*/

			/*if (DataProvider.instance.gameData.giftProbMultiplier == 1)
			{
				this.moreMystry.SetActive(true);
				this.moreMystry_Banner.SetActive(false);
			}
			else
			{
				this.moreMystry_Banner.SetActive(true);
				this.moreMystry.SetActive(false);
			}*/

			/*if (DataProvider.instance.gameData.removeAds == 1)
			{
				this.removeAds.SetActive(false);
			}
			else
			{
				this.removeAds.SetActive(true);
			}*/
			this.instantCoinsToBuy = UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f;
			this.coin_Disc.text = this.instantCoinsToBuy.ToShortHandString();
			this.megaCoinsToBuy = UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 150f;
			this.megaCoin_Disc.text = this.megaCoinsToBuy.ToShortHandString();
		}


		public void BuyStarterPack()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BuyStarterPack_REWARD");
					ads.instance.buyStarterPack = true;
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
				StarterPackPurchasedSuccessfully();
			}

			//MRInAppPurchaseManager.Instance.Purchase("starter_pack");
		}

		public void BuyMegaPack()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BuyMegaPack_REWARD");
					ads.instance.buyMegaPack = true;
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
				MegaPackPurchasedSuccessfully();
			}

			//MRInAppPurchaseManager.Instance.Purchase("mega_pack");
		}

		public void StarterPackPurchasedSuccessfully()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.starterPack);
			watchedAds++;
			if (watchedAds >= 39)
			{
				if (GUIManager.Instance.CURRENTSCREENNAME == ScreenName.StarterPack)
				{
					GUIManager.Instance.Back();
				}

				DataProvider.instance.gameData.playerCoins = DataProvider.instance.gameData.playerCoins * 3f;
				if (DataProvider.instance.gameData.currentBoxLevel < 3)
				{
					DataProvider.instance.gameData.currentBoxLevel += 2;
				}
				else
				{
					DataProvider.instance.gameData.currentBoxLevel = 4;
				}

				BoxManager.instance.UpdateBoxSprite();
				//this.RemoveAdsWorker();
				StarterPackManager.instance.StarterPackBought = true;
				UpdateShop();
				GridManager.instance.ReplaceUpdatedItems();
			}

			this.starterPackPrice.text = watchedAds + "/39";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.starterPack, watchedAds);
		}

		public void MegaPackPurchasedSuccessfully()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.megaPack);
			watchedAds++;
			if (watchedAds >= 99)
			{
				if (GUIManager.Instance.CURRENTSCREENNAME == ScreenName.StarterPack)
				{
					GUIManager.Instance.Back();
				}

				DataProvider.instance.gameData.playerCoins = DataProvider.instance.gameData.playerCoins * 6f;
				DataProvider.instance.gameData.currentBoxLevel = 4;
				BoxManager.instance.UpdateBoxSprite();
				//this.RemoveAdsWorker();
				this.MoreMysteryPurchasedSuccessfully();
				this.FasterCratesPurchasedSuccessfully();
				StarterPackManager.instance.StarterPackBought = true;
				UpdateShop();
				GridManager.instance.ReplaceUpdatedItems();
			}

			this.megaPackPrice.text = watchedAds + "/99";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.megaPack, watchedAds);
		}

		private int fillsCount;
		private int landFillAdsCount;

		public void BuyLandFill(int id)
		{
			if (AdsEnabled)
			{
				fillsCount = id;
				switch (id)
				{
					case 100:
						landFillAdsCount = 9;
						break;
					case 300:
						landFillAdsCount = 19;
						break;
					case 1000:
						landFillAdsCount = 49;
						break;
				}

				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BuyLandFill_REWARD");
					ads.instance.buyLandFill = true;
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
				fillsCount = id;
				switch (id)
				{
					case 100:
						landFillAdsCount = 9;
						break;
					case 300:
						landFillAdsCount = 19;
						break;
					case 1000:
						landFillAdsCount = 49;
						break;
				}

				FillLand();
			}

			//MRInAppPurchaseManager.Instance.Purchase("landfill_" + id);
		}

		public void FillLand()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + fillsCount);
			watchedAds++;
			if (watchedAds >= landFillAdsCount)
			{
				LandFillManager.Instance.AddLandFills(fillsCount);
				watchedAds = 0;
			}

			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.landFill + fillsCount, watchedAds);
			this.landFill100Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 100) + "/9";
			this.landFill300Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 300) + "/19";
			this.landFill1000Price.text = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 1000) + "/49";

			this.landFill100Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 100) + "/9";
			this.landFill300Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 300) + "/19";
			this.landFill1000Price_AutoFillPanel.text =
				PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.landFill + 1000) + "/49";
		}

		public void BuyCoins()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BuyCoins_REWARD");
					ads.instance.buyCoins = true;
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
				CoinsPurchasedSuccessfully();
			}

			//MRInAppPurchaseManager.Instance.Purchase("instant_coins");
		}

		public void CoinsPurchasedSuccessfully()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyCoins);
			watchedAds++;
			if (watchedAds >= 9)
			{
				//this.RemoveAdsWorker();
				DataProvider.instance.gameData.playerCoins += this.instantCoinsToBuy;
				watchedAds = 0;
			}

			this.buyCoinsPrice.text = watchedAds + "/9";
			this.buyCoinsPrice_CoinsPanel.text = watchedAds + "/9";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.buyCoins, watchedAds);
		}

		public void BuyMegaCoins()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BuyMegaCoins_REWARD");
					ads.instance.buyMegaCoins = true;
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
				MegaCoinsPurchasedSuccessfully();
			}

			//MRInAppPurchaseManager.Instance.Purchase("mega_cash");
		}

		public void MegaCoinsPurchasedSuccessfully()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.buyMegaCoins);
			watchedAds++;
			if (watchedAds >= 49)
			{
				//this.RemoveAdsWorker();
				this.megaCoinsToBuy = UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 150f;
				DataProvider.instance.gameData.playerCoins += this.megaCoinsToBuy;
				watchedAds = 0;
			}

			this.buyMegaCoinsPrice.text = watchedAds + "/49";
			this.buyMegaCoinsPrice_CoinsPanel.text = watchedAds + "/49";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.buyMegaCoins, watchedAds);
		}

		public void Button_MoreMystry()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("MoreMystry_REWARD");
					ads.instance.moreMystry = true;
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
				SetMoreMystry();
			}

			//MRInAppPurchaseManager.Instance.Purchase("more_mystery");
		}

		public void SetMoreMystry()
		{
			var watcedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.moreMystry);
			watcedAds++;
			if (watcedAds >= 19)
			{
				MoreMysteryPurchasedSuccessfully();
			}

			this.moreMystryPrice.text = watcedAds + "/19";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.moreMystry, watcedAds);
		}

		public void MoreMysteryPurchasedSuccessfully()
		{
			//this.RemoveAdsWorker();
			DataProvider.instance.gameData.giftProbMultiplier = 2;
			UpdateShop();
		}

		public void Button_MoreCoins()
		{
			//this.RemoveAdsWorker();
			DataProvider.instance.gameData.playerCoinsMultiplier = 2;
			UpdateShop();
		}

		private void RemoveAdsWorker()
		{
			PlayerPrefs.SetInt("RemoveAds", 1);
			//AdsControl.Instance.HideBanner();
			DataProvider.instance.gameData.removeAds = 1;
			//MRGame.Instance.AllAdsRemoved();
			UpdateShop();
		}

		public void Button_RemoveAds()
		{
			GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
			GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("REMOVE ADS",
				"REMOVE ADDS PERMANENTLY ?", "REMOVE", delegate
				{
					GUIManager.Instance.Back();
					this.Button_RemoveAdsClicked();
				}, null, false);
		}

		public void Button_RemoveAdsClicked()
		{
			//MRInAppPurchaseManager.Instance.Purchase("remove_ads");
		}

		public void RemoveAdsPurchasedSuccessfully()
		{
			//this.RemoveAdsWorker();
		}

		public void Button_FasterCrates()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("FasterCrates_REWARD");
					ads.instance.fasterCrates = true;
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
				SetFasterCrates();
			}

			//MRInAppPurchaseManager.Instance.Purchase("faster_crates");
		}

		public void SetFasterCrates()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.fasterCrates, 0);
			watchedAds++;
			if (watchedAds >= 19)
			{
				FasterCratesPurchasedSuccessfully();
			}

			this.fasterCratePrice.text = watchedAds + "/19";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.fasterCrates, watchedAds);
		}

		public void FasterCratesPurchasedSuccessfully()
		{
			//this.RemoveAdsWorker();
			DataProvider.instance.gameData.boxIAPMultiplier = 2;
			UpdateShop();
		}

		public void Button_UpgradeCrate()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.wasRequested = true;
					ads.instance.ShowRewardVideo("BetterCrates_REWARD");
					ads.instance.betterCrates = true;
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
				UpgradeCrate();
			}

			/*MRInAppPurchaseManager.Instance.Purchase(string.Concat(new object[]
			{
				"better_crates",
				DataProvider.instance.gameData.currentBoxLevel,
				string.Empty,
				DataProvider.instance.gameData.currentBoxLevel + 1
			}));*/
		}

		public void UpgradeCrate()
		{
			var watchedAds = PlayerPrefs.GetInt("shopWatchedAds" + ShopItems.betterCrates);
			watchedAds++;
			if (watchedAds >= 9)
			{
				DataProvider.instance.gameData.currentBoxLevel++;
				UpgradedCrateLevelSuccessfully();
				watchedAds = 0;
			}

			this.betterCrate_Price.text = watchedAds + "/9";
			PlayerPrefs.SetInt("shopWatchedAds" + ShopItems.betterCrates, watchedAds);
		}

		public void UpgradedCrateLevelSuccessfully( /*int boxValue*/)
		{
			//this.RemoveAdsWorker();
			//if (DataProvider.instance.gameData.currentBoxLevel < boxValue)
			//{
			//DataProvider.instance.gameData.currentBoxLevel = boxValue;
			BoxManager.instance.UpdateBoxSprite();
			//}
			UpdateShop();
			GridManager.instance.ReplaceUpdatedItems();
		}

		public void Button_OpenShop()
		{
			GUIManager.Instance.OpenScreenExplicitly(ScreenName.Shop);
			shopPanel.anchoredPosition = _shopPanelInitPosition;
			UpdateShop();
		}

		public void Button_RestorePurchasesClicked()
		{
			//MRUtilities.Instance.LogEvent("RestorePurchaseButtonClicked");
			//MRInAppPurchaseManager.Instance.RestorePurchases();
		}
	}
}

public enum ShopItems
{
	betterCrates,
	fasterCrates,
	moreMystry,
	buyCoins, 
	buyMegaCoins, 
	buyLandFill, 
	landFill,
	starterPack,
	megaPack
}

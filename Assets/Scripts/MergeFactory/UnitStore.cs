using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MergeFactory
{
	public class UnitStore : MonoBehaviour
	{
		public static UnitStore instance;

		public GameObject GiftBoxLocked;

		public GameObject GiftBoxUnLocked;

		public Sprite[] storeItemSprites;

		public List<Item_UnitStore> availableitems = new List<Item_UnitStore>();

		public List<Item_UnitStore> lockedItems = new List<Item_UnitStore>();
		private bool AdsEnabled;
		public bool isUnlockButtonShowed;

		private void Awake()
		{
			UnitStore.instance = this;
			this.availableitems.Clear();
			this.availableitems.Add(this.lockedItems[DataProvider.instance.gameData.currentBoxLevel]);
			for (int i = 0; i < DataProvider.instance.gameData.currentBoxLevel; i++)
			{
				this.lockedItems[i].gameObject.SetActive(false);
			}
			
			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}

		private void Start()
		{
			//gameObject.GetComponent<CanvasGroup>().alpha = 1;
			//gameObject.SetActive(false);
		}

		private void Update()
		{
			StartCoroutine(ShowFreeItemButton());
		}
		
		private IEnumerator ShowFreeItemButton()
		{
			if (UnitManager.instance.isUnlockPlantButtonActive)
			{
				lockedItems[availableitems.Count-1].free_Button.SetActive(true);
				UnitManager.instance.isUnlockPlantButtonActive = false;
				yield return new WaitForSeconds(5f);
				lockedItems[availableitems.Count-1].free_Button.SetActive(false);
			}
		}

		public void FreeGiftBox()
		{
			if (!GiftBoxTimer.instance.giftAvailable)
			{
				return;
			}
			if (GridManager.instance.hasFreeSlot())
			{
				BoxManager.instance.CreateGiftBox();
				GiftBoxTimer.instance.ResetTimer();
				GUIManager.Instance.Back();
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.GiftBoxSuccess);
			}
			else
			{
				GUIManager.Instance.Back();
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("SORRY", "NOT ENOUGH LAND!", string.Empty, null, null, false);
			}
		}

		private void OnEnable()
		{
			base.StartCoroutine(GridManager.instance.CheckForUniqueItems(0.1f));
			if (DataProvider.instance == null)
			{
				return;
			}
			//ShopManager.instance.ShowInterstitialAd();
			if (LevelManager.instance.currentLevel.levelNo < 5)
			{
				this.GiftBoxLocked.SetActive(true);
				this.GiftBoxUnLocked.SetActive(false);
			}
			else
			{
				this.GiftBoxLocked.SetActive(false);
				this.GiftBoxUnLocked.SetActive(true);
				StoreChangeIcon.Instance.ShowStoreChangeIcon(true);
			}
			if (!this.availableitems.Contains(this.lockedItems[DataProvider.instance.gameData.currentBoxLevel]))
			{
				this.availableitems.Add(this.lockedItems[DataProvider.instance.gameData.currentBoxLevel]);
			}
			for (int i = 0; i < DataProvider.instance.gameData.currentBoxLevel; i++)
			{
				this.lockedItems[i].gameObject.SetActive(false);
			}
			for (int j = 0; j < this.lockedItems.Count; j++)
			{
				if (this.lockedItems[j].unit.id <= DataProvider.instance.gameData.highestUnitUnlocked - 3 && !this.availableitems.Contains(this.lockedItems[j]))
				{
					this.availableitems.Add(this.lockedItems[j]);
				}
				this.lockedItems[j].SetItem(false, false, false);
			}
			for (int k = 0; k < this.availableitems.Count; k++)
			{
				if (this.availableitems[k].unit.id <= DataProvider.instance.gameData.currentBoxLevel)
				{
					this.availableitems[k].gameObject.SetActive(false);
				}
				this.availableitems[k].SetItem(true, false, false);
			}
			if (this.availableitems[this.availableitems.Count - 1].unit.id > DataProvider.instance.gameData.currentBoxLevel + 1 && StoreChangeIcon.Instance.storeItemStatus)
			{
				this.availableitems[this.availableitems.Count - 1].SetItem(true, false, true);
			}
			if (this.availableitems.Count >= 3)
			{
				this.availableitems[this.availableitems.Count - 3].SetItem(true, true, false);
			}
			//SetUnlockButton();
		}

		public void UnlockItem()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.ShowRewardVideo("UnlockStoreItem_REWARD");
					ads.instance.wasRequested = true;
					ads.instance.unlockStoreItem = true;
				}
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				}
			}
			else
			{
				UnlockStoreItem();
			}
		}

		public void UnlockStoreItem()
		{
			this.lockedItems[this.availableitems.Count].unlockButton.SetActive(false);
			DataProvider.instance.gameData.highestUnitUnlocked =
				this.lockedItems[this.availableitems.Count].unit.id + 3;
			OnEnable();
			//SetUnlockButton();
			DataProvider.instance.gameData.isStoreUnlockButtonActive = 0; //false
		}

		public void SetUnlockButton()
		{
			//if (availableitems.Count < lockedItems.Count && DataProvider.instance.gameData.highestUnitUnlocked>=5)
			if (availableitems.Count < lockedItems.Count && DataProvider.instance.gameData.isStoreUnlockButtonActive>=1)
			{
				this.lockedItems[this.availableitems.Count - 1].unlockButton.SetActive(false);
				this.lockedItems[this.availableitems.Count].unlockButton.SetActive(!lockedItems[this.availableitems.Count].buy_Button.activeSelf);
				this.lockedItems[this.availableitems.Count].locked_Button.SetActive(false);
			}
		}
	}
}

using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace MergeFactory
{
	public class Box : MonoBehaviour
	{
		private sealed class _AnimateBox_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Box _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get
				{
					return this._current;
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return this._current;
				}
			}

			public _AnimateBox_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(2.5f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					//this._this.animator.Play("Animation_BoxRotation");
					this._this.StartCoroutine(this._this.AnimateBox());
					this._PC = -1;
					break;
				}
				return false;
			}

			public void Dispose()
			{
				this._disposing = true;
				this._PC = -1;
			}

			public void Reset()
			{
				throw new NotSupportedException();
			}
		}

		private sealed class _ShowGiftBox_c__AnonStorey1
		{
			internal int unitID;

			internal Box _this;

			//private static MRUtilities.RewardedVideoAction __f__am_cache0;

			internal void __m__0()
			{
                //MRUtilities.Instance.LogEvent("GiftBoxUpgradeButtonClicked");
                //FireBaseManager.Instance.LogScreen("GiftBoxUpgradeButtonClicked");


                //if (AdsControl.Instance.GetRewardAvailable())
                //{
                //    AdsControl.Instance.PlayDelegateRewardVideo(delegate
                //    {
                //        int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
                //        this._this.ShowGiftBox(num);
                //        FireBaseManager.Instance.LogScreen("VideoNotAvailableGiftBoxUpgrade");
                //    });
                //}
                //else
                //{
                //    GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                //    GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
                //    FireBaseManager.Instance.LogScreen("GiftBoxUpgradeButtonClicked");
                //}

                /*
				MRUtilities.Instance.ShowVideoAd(delegate
				{
					int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
					this._this.ShowGiftBox(num);
					MRUtilities.Instance.LogEvent("GiftBoxUpgraded");
				}, delegate
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
					MRUtilities.Instance.LogEvent("VideoNotAvailableGiftBoxUpgrade");
				});
				*/
            }

			internal void __m__1()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__2()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__3()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__4()
			{
				int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
				this._this.ShowGiftBox(num);
                //MRUtilities.Instance.LogEvent("GiftBoxUpgraded");
                //FireBaseManager.Instance.LogScreen("GiftBoxUpgraded");
            }

			private static void __m__5()
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
                //MRUtilities.Instance.LogEvent("VideoNotAvailableGiftBoxUpgrade");
                //FireBaseManager.Instance.LogScreen("VideoNotAvailableGiftBoxUpgrade");

            }
		}

		private sealed class _ShowMystryBox_c__AnonStorey2
		{
			internal int unitID;

			internal Box _this;

		//private static MRUtilities.RewardedVideoAction __f__am_cache0;

			internal void __m__0()
			{
                /*
				MRUtilities.Instance.LogEvent("MysteryBoxUpgradeButtonClicked");
				MRUtilities.Instance.ShowVideoAd(delegate
				{
					int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
					this._this.ShowMystryBox(num);
					MRUtilities.Instance.LogEvent("MysteryBoxUpgraded");
				}, delegate
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
					MRUtilities.Instance.LogEvent("VideoNotAvailableMysteryBoxUpgrade");
				});
				*/

                //if (AdsControl.Instance.GetRewardAvailable())
                //{
                //    AdsControl.Instance.PlayDelegateRewardVideo(delegate
                //    {

                //        int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
                //        this._this.ShowMystryBox(num);
                //       // MRUtilities.Instance.LogEvent("MysteryBoxUpgraded");
                //        FireBaseManager.Instance.LogScreen("MysteryBoxUpgraded");
                //    });
                //}
                //else
                //{
                //    GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                //    GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
                //    // MRUtilities.Instance.LogEvent("VideoNotAvailableMysteryBoxUpgrade");
                //    FireBaseManager.Instance.LogScreen("VideoNotAvailableMysteryBoxUpgrade");
                //}
            }

			internal void __m__1()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__2()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__3()
			{
				UnitManager.instance.SpawnUnit(this._this.slot, this.unitID);
				ParticleManager.instance.ShowBoxOpenedParticle(this._this.transform.position);
				GUIManager.Instance.Back();
				UnityEngine.Object.Destroy(this._this.gameObject);
			}

			internal void __m__4()
			{
				int num = UnityEngine.Random.Range(this.unitID + 1, UnitManager.instance.MaxItemID);
				this._this.ShowMystryBox(num);
                //MRUtilities.Instance.LogEvent("MysteryBoxUpgraded");
                //FireBaseManager.Instance.LogScreen("MysteryBoxUpgraded");
            }

			private static void __m__5()
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				//MRUtilities.Instance.LogEvent("VideoNotAvailableMysteryBoxUpgrade");
                //FireBaseManager.Instance.LogScreen("VideoNotAvailableMysteryBoxUpgrade");
            }
		}

		public BoxType boxType;

		private TweenRotation tween;

		public int boxlevel;

		private Animator animator;

		public Unit _unit;

		public int mystryBoxDLGCount;

		public int giftBoxDLGCount;

		public int HighestItemInStore
		{
			get
			{
				return DataProvider.instance.gameData.highestUnitUnlocked - 4;
			}
		}

		public Unit unit
		{
			get
			{
				return this._unit;
			}
			set
			{
				if (this.slot != null)
				{
					this.slot.slotData.unit = value;
				}
				this._unit = value;
			}
		}

		public Slot slot
		{
			get
			{
				if (base.GetComponentInParent<Slot>())
				{
					return base.GetComponentInParent<Slot>();
				}
				return null;
			}
		}

		public void AdjustSize()
		{
			base.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
			base.GetComponent<RectTransform>().anchoredPosition = BoxManager.instance.offset;
		}

		private IEnumerator AnimateBox()
		{
			Box._AnimateBox_c__Iterator0 _AnimateBox_c__Iterator = new Box._AnimateBox_c__Iterator0();
			_AnimateBox_c__Iterator._this = this;
			return _AnimateBox_c__Iterator;
		}

		private void OnEnable()
		{
			this.animator = base.GetComponent<Animator>();
			this.tween = base.GetComponent<TweenRotation>();
			base.transform.rotation = Quaternion.identity;
			base.StartCoroutine(this.AnimateBox());
		}

		private void ShowGiftBox(int unitID)
		{
			this.giftBoxDLGCount++;
			if (unitID < this.HighestItemInStore && this.giftBoxDLGCount <= 4)
			{
				GUIManager.Instance.CURRENTPANEL.GetComponent<GiftboxPanel>().ShowGift("GIFT BOX", unitID, "WATCH AN ADD TO UPGRADE IT", delegate
				{
					//MRUtilities.Instance.LogEvent("GiftBoxUpgradeButtonClicked");
					/*
					MRUtilities.Instance.ShowVideoAd(delegate
					{
						int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
						this.ShowGiftBox(unitID2);
						MRUtilities.Instance.LogEvent("GiftBoxUpgraded");
					}, delegate
					{
						GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
						GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
						MRUtilities.Instance.LogEvent("VideoNotAvailableGiftBoxUpgrade");
					});
					*/

					int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
					this.ShowGiftBox(unitID2);

					//if (AdsControl.Instance.GetRewardAvailable())
     //               {
     //                   AdsControl.Instance.PlayDelegateRewardVideo(delegate
     //                   {
     //                       int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
     //                       this.ShowGiftBox(unitID2);
     //                       //FireBaseManager.Instance.LogScreen("GiftBoxUpgraded");
     //                   });
     //               }
     //               else
     //               {
     //                   GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
     //                   GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
     //                   //FireBaseManager.Instance.LogScreen("VideoNotAvailableGiftBoxUpgrade");
     //               }


                }, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				});
			}
			else
			{
				if (this.giftBoxDLGCount > 4)
				{
					unitID = UnitManager.instance.MaxItemID;
				}
				GUIManager.Instance.CURRENTPANEL.GetComponent<GiftboxPanel>().ShowGift("GIFT BOX", unitID, string.Empty, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				}, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				});
			}
		}

		private void ShowMystryBox(int unitID)
		{
			this.mystryBoxDLGCount++;
			if (unitID < this.HighestItemInStore && this.mystryBoxDLGCount <= 4)
			{
				GUIManager.Instance.CURRENTPANEL.GetComponent<GiftboxPanel>().ShowGift("MYSTRY BOX", unitID, "WATCH AN ADD TO UPGRADE IT", delegate
				{
					//	MRUtilities.Instance.LogEvent("MysteryBoxUpgradeButtonClicked");
					//FireBaseManager.Instance.LogScreen("MysteryBoxUpgradeButtonClicked");
					/*
                    MRUtilities.Instance.ShowVideoAd(delegate
					{
						int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
						this.ShowMystryBox(unitID2);
						MRUtilities.Instance.LogEvent("MysteryBoxUpgraded");
					}, delegate
					{
						GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
						GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
						MRUtilities.Instance.LogEvent("VideoNotAvailableMysteryBoxUpgrade");
					});
					*/
					int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
					this.ShowMystryBox(unitID2);

					//if (AdsControl.Instance.GetRewardAvailable())
     //               {
     //                   AdsControl.Instance.PlayDelegateRewardVideo(delegate
     //                   {
     //                       int unitID2 = UnityEngine.Random.Range(unitID + 1, UnitManager.instance.MaxItemID);
     //                       this.ShowMystryBox(unitID2);
     //                       // MRUtilities.Instance.LogEvent("MysteryBoxUpgraded");
     //                       //FireBaseManager.Instance.LogScreen("MysteryBoxUpgraded");
     //                   });
     //               }
     //               else
     //               {
     //                   GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
     //                   GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
     //                   // MRUtilities.Instance.LogEvent("VideoNotAvailableMysteryBoxUpgrade");
     //                   //FireBaseManager.Instance.LogScreen("VideoNotAvailableMysteryBoxUpgrade");
     //               }

                }, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				});
			}
			else
			{
				if (this.mystryBoxDLGCount > 4)
				{
					unitID = UnitManager.instance.MaxItemID;
				}
				GUIManager.Instance.CURRENTPANEL.GetComponent<GiftboxPanel>().ShowGift("MYSTRY BOX", unitID, string.Empty, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				}, delegate
				{
					UnitManager.instance.SpawnUnit(this.slot, unitID);
					ParticleManager.instance.ShowBoxOpenedParticle(this.transform.position);
					GUIManager.Instance.Back();
					UnityEngine.Object.Destroy(this.gameObject);
				});
			}
		}
		
		public void ShowInterAd()
		{
			IronSource.Agent.loadInterstitial();
			if (IronSource.Agent.isInterstitialReady())
			{
				IronSource.Agent.showInterstitial();
			}
		}

		public void BoxClick_Button()
		{
			SFXManager.instance.PlaySound(Sound.BoxTap);
			switch (this.boxType)
			{
			case BoxType.Normal:
				AchievementsManager.instance.IncrementAchievementEvent(AchievementType.OPENCRATE);
                    //MRUtilities.Instance.LogEvent("BoxOpened", "Type", "NormalBox" + this.slot.slotData.boxLevel);
                    //FireBaseManager.Instance.LogScreen("BoxOpened");
                    ParticleManager.instance.ShowBoxOpenedParticle(base.transform.position);
				UnitManager.instance.SpawnUnit(this.slot, this.boxlevel);
				if (this.boxlevel > 0)
				{
					ParticleManager.instance.LevelIncShowParticle(base.transform.position, this.boxlevel);
				}
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			case BoxType.Mystry:
			{
				/*var openedMistryCount = ads.instance.openedMistryBoxCount;
				openedMistryCount++;
				if (openedMistryCount >= 10) //show interstitial before 10 opened mistry boxes 
				{
					ShowInterAd();
					openedMistryCount = 0;
				}
				ads.instance.openedMistryBoxCount = openedMistryCount;*/
				
				AchievementsManager.instance.IncrementAchievementEvent(AchievementType.OPENMYSTERY);
                        //MRUtilities.Instance.LogEvent("BoxOpened", "Type", "MysteryBox");
                        //FireBaseManager.Instance.LogScreen("BoxOpened");
                        GUIManager.Instance.OpenScreenExplicitly(ScreenName.MystryGiftBox);
				int unitID = UnityEngine.Random.Range(UnitManager.instance.MinItemID, UnitManager.instance.MaxItemID);
				this.mystryBoxDLGCount = 0;
				this.ShowMystryBox(unitID);
				DailyTasksController.instance.IncrementDailyTaskEvent(DailyTaskType.OPENMYSTERYBOX);
				break;
			} 
			case BoxType.Gift:
			{
				//MRUtilities.Instance.LogEvent("BoxOpened", "Type", "GiftBox");
                        //FireBaseManager.Instance.LogScreen("BoxOpened");
                        GUIManager.Instance.OpenScreenExplicitly(ScreenName.MystryGiftBox);
				int unitID = UnityEngine.Random.Range(UnitManager.instance.MinItemID, UnitManager.instance.MaxItemID);
				this.giftBoxDLGCount = 0;
				this.ShowGiftBox(unitID);
				break;
			}
			case BoxType.UnitSpawner:
				//MRUtilities.Instance.LogEvent("BoxOpened", "Type", "NormalBox0");
                    //FireBaseManager.Instance.LogScreen("BoxOpened");
                    UnitManager.instance.SpawnUnit(this.slot, this.unit.id);
				ParticleManager.instance.ShowBoxOpenedParticle(base.transform.position);
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			case BoxType.FreeVideo:
				//MRUtilities.Instance.LogEvent("BoxOpened", "Type", "VideoBox");
                    //FireBaseManager.Instance.LogScreen("BoxOpened");
                    UnitManager.instance.SpawnUnit(this.slot, this.unit.id);
				ParticleManager.instance.ShowBoxOpenedParticle(base.transform.position);
				UnityEngine.Object.Destroy(base.gameObject);
				break;
			}
		}
	}
}

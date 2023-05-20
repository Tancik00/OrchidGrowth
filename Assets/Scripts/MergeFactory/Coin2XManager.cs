using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MergeFactory
{
	public class Coin2XManager : MonoBehaviour
	{
		private sealed class _ShowEarnings_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			private sealed class _ShowEarnings_c__AnonStorey3
			{
				internal float reward;

				internal Coin2XManager._ShowEarnings_c__Iterator0 __f__ref_0;

				//private static MRUtilities.RewardedVideoAction __f__am_cache0;

				internal void __m__0()
				{
                    /*
					if (MRUtilities.Instance)
					{
						MRUtilities.Instance.LogEvent("DoubleRewardButtonClicked");
					}
                    */
                    //FireBaseManager.Instance.LogScreen("DoubleRewardButtonClicked");
                    //if (AdsControl.Instance.GetRewardAvailable())
                    //{
                    //    AdsControl.Instance.PlayDelegateRewardVideo(delegate
                    //    {

                    //        // MRUtilities.Instance.LogEvent("DoubleRewardEarned", "AmmountEarned", (this.reward * 2f).ToString());
                    //        FireBaseManager.Instance.LogScreen("DoubleRewardEarned");
                    //        DataProvider.instance.gameData.playerCoins += this.reward * 2f;
                    //        GUIManager.Instance.Back();
                    //        GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                    //        GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("DOUBLED", "Ammount Earned : " + (this.reward * 2f).ToShortHandString(), string.Empty, null, null, false);
                    //    });
                    //}
                    //else
                    //{
                    //    GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
                    //    GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
                    //    //FireBaseManager.Instance.LogScreen("VideoNotAvailableDoubleReward");
                    //    // MRUtilities.Instance.LogEvent("VideoNotAvailableDoubleReward");

                    //}
                    /*
                    MRUtilities.Instance.ShowVideoAd(delegate
					{
						if (MRUtilities.Instance)
						{
							MRUtilities.Instance.LogEvent("DoubleRewardEarned", "AmmountEarned", (this.reward * 2f).ToString());
						}
						DataProvider.instance.gameData.playerCoins += this.reward * 2f;
						GUIManager.Instance.Back();
						GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
						GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("DOUBLED", "Ammount Earned : " + (this.reward * 2f).ToShortHandString(), string.Empty, null, null, false);
					}, delegate
					{
						GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
						GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
						if (MRUtilities.Instance)
						{
							MRUtilities.Instance.LogEvent("VideoNotAvailableDoubleReward");
						}
					});
					*/
                }

				internal void __m__1()
				{
					DataProvider.instance.gameData.playerCoins += this.reward;
					GUIManager.Instance.Back();
					print("Sets the player money without reward.");
				}

				internal void __m__2()
				{
                    /*
					if (MRUtilities.Instance)
					{
						MRUtilities.Instance.LogEvent("DoubleRewardEarned", "AmmountEarned", (this.reward * 2f).ToString());
					}
					*/
                    //FireBaseManager.Instance.LogScreen("DoubleRewardEarned");
                    DataProvider.instance.gameData.playerCoins += this.reward * 2f;
					GUIManager.Instance.Back();
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("DOUBLED", "Ammount Earned : " + (this.reward * 2f).ToShortHandString(), string.Empty, null, null, false);
					print("Sets the player money doubled.");
				}

				private static void __m__3()
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
					/*
                    if (MRUtilities.Instance)
					{
						MRUtilities.Instance.LogEvent("VideoNotAvailableDoubleReward");
					}
                    */
					//FireBaseManager.Instance.LogScreen("VideoNotAvailableDoubleReward");
					print("Video is not available.");
				}
			}

			internal Coin2XManager _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			private Coin2XManager._ShowEarnings_c__Iterator0._ShowEarnings_c__AnonStorey3 _locvar0;

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

			public _ShowEarnings_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				print("Move Next Num: " + num);
				switch (num)
				{
				case 0u:
					this._locvar0 = new Coin2XManager._ShowEarnings_c__Iterator0._ShowEarnings_c__AnonStorey3();
					this._locvar0.__f__ref_0 = this;
					this._current = new WaitForSeconds(0.05f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._locvar0.reward = (float)(this._this.rewardSeconds * (double)GridManager.instance.CoinsPerSec);
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.WelcomeBack);
					GUIManager.Instance.CURRENTPANEL.GetComponent<WelcomeBackScreen>().ShowMessage(this._locvar0.reward, new WelcomeBackScreen.ConfirmationActions(this._locvar0.__m__2), new WelcomeBackScreen.ConfirmationActions(this._locvar0.__m__1));
					this._this.ShowBackButton();
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

		private sealed class _CheckLandFill_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal double timeElapsed;

			internal int _slotsToFill___0;

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

			public _CheckLandFill_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(0.25f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					this._slotsToFill___0 = (int)(this.timeElapsed / (double)BoxManager.instance.boxSpawnInterval);
					if (this._slotsToFill___0 > GridManager.instance.FREESLOTS)
					{
						this._slotsToFill___0 = GridManager.instance.FREESLOTS;
					}
					BoxManager.instance.FillLand(this._slotsToFill___0);
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

		private sealed class _Coin2XUpdateWorker_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Coin2XManager _this;

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

			public _Coin2XUpdateWorker_c__Iterator2()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					break;
				case 1u:
					break;
				default:
					return false;
				}
				this._this.currentDateTime = DateTime.Now;
				PlayerPrefs.SetString("currentTime2X", this._this.currentDateTime.ToString());
				this._this.UpdateUI();
				if (this._this.remainingSeconds > 0.0)
				{
					this._this.remainingSeconds -= 1.0;
				}
				else if (DataProvider.instance.gameData.playerCoinsMultiplier == 2)
				{
					DataProvider.instance.gameData.playerCoinsMultiplier = 1;
					if (PlayerPrefs.HasKey("targetTime2X"))
					{
						PlayerPrefs.DeleteKey("targetTime2X");
					}
				}
				this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
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

		public static Coin2XManager instance;

		public double maxHours = 8.0;

		public double startRewardMaxHours = 3.0;

		public double rewardSeconds;

		public double remainingSeconds;

		private TimeSpan maxTime;

		private DateTime targetDateTime;

		private DateTime currentDateTime;

		private TimeSpan remainingTime;

		private bool AdsEnabled;

		[Header("UI Elements")]
		public Text timeText;

		public Image timeBar;
		public GameObject backButton;

//		private static MRUtilities.RewardedVideoAction __f__am_cache0;

		private void Awake()
		{
			Coin2XManager.instance = this;
			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
			this.remainingTime = TimeSpan.FromSeconds(0.0);
			this.maxTime = TimeSpan.FromHours(this.maxHours);
		}

		private void Start()
		{
			base.StartCoroutine(this.Coin2XUpdateWorker());
		}

		private void UpdateUI()
		{
			if (this.remainingSeconds <= 0.0)
			{
				this.timeBar.fillAmount = 0f;
				this.timeText.text = "00:00:00";
			}
			else
			{
				this.timeBar.fillAmount = (float)(this.remainingSeconds / this.maxTime.TotalSeconds);
				this.timeText.text = this.remainingSeconds.ToTimeString();
			}
		}

		public void DoubleCoinsReward()
        {
			if (PlayerPrefs.HasKey("targetTime2X"))
			{
				this.targetDateTime = DateTime.Parse(PlayerPrefs.GetString("targetTime2X"));
			}
			else
			{
				this.targetDateTime = DateTime.Now;
			}
			this.targetDateTime = this.targetDateTime.AddHours(2.0);
			PlayerPrefs.SetString("targetTime2X", this.targetDateTime.ToString());
			this.remainingSeconds = (this.targetDateTime - DateTime.Now).TotalSeconds;
			if (this.remainingSeconds <= 0.0)
			{
				DataProvider.instance.gameData.playerCoinsMultiplier = 1;
			}
			else
			{
				DataProvider.instance.gameData.playerCoinsMultiplier = 2;
			}
		}

		public void DoubleCoins()
		{
			if ((this.targetDateTime - DateTime.Now).TotalHours > 6.0)
			{
				return;
			}

            //FireBaseManager.Instance.LogScreen("CoinDoublerButtonClicked");
			if(AdsEnabled)
            {
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.ShowRewardVideo("DoubleCoins_REWARDED");
				}
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
					//FireBaseManager.Instance.LogScreen("VideoNotAvailableCoinDoubler");
				}
			}
			else
            {
				DoubleCoinsReward();
			}

            

            /*
            if (MRUtilities.Instance)
        {
            MRUtilities.Instance.LogEvent("CoinDoublerButtonClicked");
        }
        MRUtilities.Instance.ShowVideoAd(delegate
        {
            MRUtilities.Instance.LogEvent("CoinDoublerEarned");
            if (PlayerPrefs.HasKey("targetTime2X"))
            {
                this.targetDateTime = DateTime.Parse(PlayerPrefs.GetString("targetTime2X"));
            }
            else
            {
                this.targetDateTime = DateTime.Now;
            }
            this.targetDateTime = this.targetDateTime.AddHours(2.0);
            PlayerPrefs.SetString("targetTime2X", this.targetDateTime.ToString());
            this.remainingSeconds = (this.targetDateTime - DateTime.Now).TotalSeconds;
            if (this.remainingSeconds <= 0.0)
            {
                DataProvider.instance.gameData.playerCoinsMultiplier = 1;
            }
            else
            {
                DataProvider.instance.gameData.playerCoinsMultiplier = 2;
            }
        }, delegate
        {
            GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
            GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
            if (MRUtilities.Instance)
            {
                MRUtilities.Instance.LogEvent("VideoNotAvailableCoinDoubler");
            }
        });
        */
        }

		private IEnumerator ShowEarnings()
		{
			Coin2XManager._ShowEarnings_c__Iterator0 _ShowEarnings_c__Iterator = new Coin2XManager._ShowEarnings_c__Iterator0();
			_ShowEarnings_c__Iterator._this = this;
			return _ShowEarnings_c__Iterator;
		}

		private IEnumerator CheckLandFill(double timeElapsed)
		{
			Coin2XManager._CheckLandFill_c__Iterator1 _CheckLandFill_c__Iterator = new Coin2XManager._CheckLandFill_c__Iterator1();
			_CheckLandFill_c__Iterator.timeElapsed = timeElapsed;
			return _CheckLandFill_c__Iterator;
		}

		private void OnStart()
		{
			this.currentDateTime = DateTime.Now;
			this.targetDateTime = DateTime.Now;
			if (PlayerPrefs.HasKey("currentTime2X"))
			{
				this.currentDateTime = DateTime.Parse(PlayerPrefs.GetString("currentTime2X"));
			}
			if (TutorialManager.instance.TUT_1_COMPLETE)
			{
				base.StartCoroutine(this.CheckLandFill((DateTime.Now - this.currentDateTime).TotalSeconds));
			}
			if (PlayerPrefs.HasKey("targetTime2X"))
			{
				this.targetDateTime = DateTime.Parse(PlayerPrefs.GetString("targetTime2X"));
				this.remainingSeconds = (this.targetDateTime - DateTime.Now).TotalSeconds;
			}
			else
			{
				this.remainingSeconds = 0.0;
			}
			if (this.remainingSeconds <= 0.0)
			{
				this.remainingSeconds = 0.0;
			}
			this.rewardSeconds = (DateTime.Now - this.currentDateTime).TotalSeconds;
			if (this.rewardSeconds < 0.0)
			{
				this.rewardSeconds = 0.0;
			}
			else if (this.rewardSeconds > 100.0)
			{
				if (this.rewardSeconds > TimeSpan.FromHours(this.startRewardMaxHours).TotalSeconds)
				{
					this.rewardSeconds = TimeSpan.FromHours(this.startRewardMaxHours).TotalSeconds;
				}
				if (TutorialManager.instance.TUTCompleted)
				{
					base.StartCoroutine(this.ShowEarnings());
				}
			}
			if (this.remainingSeconds > 0.0)
			{
				DataProvider.instance.gameData.playerCoinsMultiplier = 2;
			}
			else
			{
				if (PlayerPrefs.HasKey("targetTime2X"))
				{
					PlayerPrefs.DeleteKey("targetTime2X");
				}
				this.targetDateTime = DateTime.Now;
				DataProvider.instance.gameData.playerCoinsMultiplier = 1;
			}
		}

		private void OnApplicationPause(bool paused)
		{
			if (!paused)
			{
				this.AppUnpausedTask();
			}
			if (paused)
			{
			}
		}

		private void AppUnpausedTask()
		{
			this.OnStart();
			GiftBoxTimer.instance.CheckTimer();
			//StarterPackManager.instance.UpdateStarterPack();
		}

		private IEnumerator Coin2XUpdateWorker()
		{
			Coin2XManager._Coin2XUpdateWorker_c__Iterator2 _Coin2XUpdateWorker_c__Iterator = new Coin2XManager._Coin2XUpdateWorker_c__Iterator2();
			_Coin2XUpdateWorker_c__Iterator._this = this;
			return _Coin2XUpdateWorker_c__Iterator;
		}

		public void ShowBackButton()
		{
			backButton.SetActive(false);
			StartCoroutine(ShowBackButtonBeforeDelay());
		}

		private IEnumerator ShowBackButtonBeforeDelay()
		{
			yield return new WaitForSeconds(0.5f);
			backButton.SetActive(true);
		}
	}
}

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
	public class AchievementsManager : MonoBehaviour
	{
		private sealed class _CreateAchievementItems_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			private sealed class _CreateAchievementItems_c__AnonStorey1
			{
				internal AchievementSet achSet;

				internal AchievementsManager._CreateAchievementItems_c__Iterator0 __f__ref_0;
			}

			private sealed class _CreateAchievementItems_c__AnonStorey2
			{
				internal int index;

				internal float reward;

				internal AchievementsManager._CreateAchievementItems_c__Iterator0 __f__ref_0;

				internal AchievementsManager._CreateAchievementItems_c__Iterator0._CreateAchievementItems_c__AnonStorey1
					__f__ref_1;

				internal void __m__0()
				{
					if (this.__f__ref_0._this.IsAchievementProgressCompleted(
						this.__f__ref_1.achSet.ACHIEVEMENTS[this.index]))
					{
						DataProvider.instance.gameData.playerCoins += this.reward;
						this.__f__ref_1.achSet.ACHIEVEMENTS[this.index].isACHIEVED = true;
						//MRUtilities.Instance.LogEvent("Achievements", "AchivementRewardClaimed", this.__f__ref_1.achSet.ACHIEVEMENTS[this.index].NAME);
						SFXManager.instance.PlaySound(10);
						this.__f__ref_0._this.StartCoroutine(this.__f__ref_0._this.CreateAchievementItems(0f));
					}
					else
					{
						GUIManager.Instance.OpenScreenExplicitly(ScreenName.CoinsShop);
					}
				}
			}

			internal float p_waitTime;

			internal IEnumerator _locvar0;

			internal IDisposable _locvar1;

			internal List<AchievementSet>.Enumerator _locvar2;

			internal AchievementsManager _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			object IEnumerator<object>.Current
			{
				get { return this._current; }
			}

			object IEnumerator.Current
			{
				get { return this._current; }
			}

			public _CreateAchievementItems_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint) this._PC;
				this._PC = -1;
				switch (num)
				{
					case 0u:
						this._current = new WaitForSeconds(this.p_waitTime);
						if (!this._disposing)
						{
							this._PC = 1;
						}

						return true;
					case 1u:
						this._this.CaterAchivementsButton();
						this._locvar0 = this._this.gridRoot.transform.GetEnumerator();
						try
						{
							while (this._locvar0.MoveNext())
							{
								Transform transform = (Transform) this._locvar0.Current;
								UnityEngine.Object.Destroy(transform.gameObject);
							}
						}
						finally
						{
							if ((this._locvar1 = (this._locvar0 as IDisposable)) != null)
							{
								this._locvar1.Dispose();
							}
						}

						this._locvar2 = this._this.achievementSets.GetEnumerator();
						try
						{
							while (this._locvar2.MoveNext())
							{
								AchievementSet achSet = this._locvar2.Current;
								GameObject gameObject =
									UnityEngine.Object.Instantiate<GameObject>(this._this.achievementItemPrefab,
										this._this.gridRoot.transform);
								gameObject.transform.localPosition = Vector3.zero;
								gameObject.transform.localScale = Vector3.one;
								if (achSet.isACHIEVEMENTSETCOMPLETE)
								{
									gameObject.transform.Find("Image_AchivementRibbon").GetComponent<Image>().sprite =
										this._this.ribbon3;
									gameObject.transform.Find("Button_Reward").GetChild(0).gameObject.SetActive(false);
									gameObject.transform.Find("Button_Reward").GetChild(1).gameObject.SetActive(false);
									gameObject.transform.Find("Button_Reward").GetChild(2).gameObject.SetActive(false);
									gameObject.transform.Find("Button_Reward").GetComponent<Button>().image.sprite =
										this._this.achievementSetComplete;
									gameObject.transform.Find("Star1").GetComponent<Image>().sprite =
										this._this.starGold;
									gameObject.transform.Find("Star2").GetComponent<Image>().sprite =
										this._this.starGold;
									gameObject.transform.Find("Star3").GetComponent<Image>().sprite =
										this._this.starGold;
									gameObject.transform.Find("Text_AchievementName").GetComponent<Text>().text =
										achSet.ACHIEVEMENTS[2].NAME;
									gameObject.transform.Find("Text_AchivementDescription").GetComponent<Text>().text =
										achSet.ACHIEVEMENTS[2].DESCRIPTION;
									gameObject.name = achSet.ACHIEVEMENTS[2].ID.ToString();
									this._this.SetProgress(achSet.ACHIEVEMENTS[2], gameObject);
								}
								else
								{
									int index = achSet.CURRENTACHIEVEMENTINDEX;
									gameObject.name = achSet.ACHIEVEMENTS[index].ID.ToString();
									gameObject.transform.Find("Star1").GetComponent<Image>().sprite =
										this._this.starGrey;
									gameObject.transform.Find("Star2").GetComponent<Image>().sprite =
										this._this.starGrey;
									gameObject.transform.Find("Star3").GetComponent<Image>().sprite =
										this._this.starGrey;
									if (index == 0)
									{
										gameObject.transform.Find("Image_AchivementRibbon").GetComponent<Image>().sprite
											= this._this.ribbon1;
										gameObject.transform.Find("Star1").GetComponent<Image>().sprite =
											this._this.starGrey;
										gameObject.transform.Find("Star2").GetComponent<Image>().sprite =
											this._this.starGrey;
										gameObject.transform.Find("Star3").GetComponent<Image>().sprite =
											this._this.starGrey;
									}
									else if (index == 1)
									{
										gameObject.transform.Find("Image_AchivementRibbon").GetComponent<Image>().sprite
											= this._this.ribbon2;
										gameObject.transform.Find("Star1").GetComponent<Image>().sprite =
											this._this.starGold;
										gameObject.transform.Find("Star2").GetComponent<Image>().sprite =
											this._this.starGrey;
										gameObject.transform.Find("Star3").GetComponent<Image>().sprite =
											this._this.starGrey;
									}
									else if (index == 2)
									{
										gameObject.transform.Find("Image_AchivementRibbon").GetComponent<Image>().sprite
											= this._this.ribbon3;
										gameObject.transform.Find("Star1").GetComponent<Image>().sprite =
											this._this.starGold;
										gameObject.transform.Find("Star2").GetComponent<Image>().sprite =
											this._this.starGold;
										gameObject.transform.Find("Star3").GetComponent<Image>().sprite =
											this._this.starGrey;
									}

									gameObject.transform.Find("Button_Reward").GetChild(0).gameObject.SetActive(true);
									gameObject.transform.Find("Button_Reward").GetChild(1).gameObject.SetActive(true);
									gameObject.transform.Find("Button_Reward").GetChild(2).gameObject.SetActive(true);
									float reward = GridManager.instance.CoinsPerSec * achSet.ACHIEVEMENTS[index].REWARD;
									gameObject.transform.Find("Button_Reward").GetChild(2).gameObject
										.GetComponent<Text>().text = reward.ToShortHandString();
									if (this._this.IsAchievementProgressCompleted(achSet.ACHIEVEMENTS[index]))
									{
										gameObject.transform.Find("Button_Reward").GetComponent<Button>().image.sprite =
											this._this.rewardClaim;
									}
									else
									{
										gameObject.transform.Find("Button_Reward").GetComponent<Button>().image.sprite =
											this._this.rewardNotAvailble;
									}

									gameObject.transform.Find("Text_AchievementName").GetComponent<Text>().text =
										achSet.ACHIEVEMENTS[index].NAME;
									gameObject.transform.Find("Text_AchivementDescription").GetComponent<Text>().text =
										achSet.ACHIEVEMENTS[index].DESCRIPTION;
									this._this.SetProgress(achSet.ACHIEVEMENTS[index], gameObject);
									gameObject.transform.Find("Button_Reward").GetComponent<Button>().onClick
										.AddListener(delegate
										{
											if (DataProvider.instance.gameData.currentBoxLevel < 19)
											{
												if (achSet.TYPE == AchievementType.WATCHADS)
												{
													DataProvider.instance.gameData.currentBoxLevel++;
													BoxManager.instance.UpdateBoxSprite();
												}
											}
											if (this._this.IsAchievementProgressCompleted(achSet.ACHIEVEMENTS[index]))
											{
												DataProvider.instance.gameData.playerCoins += reward;
												achSet.ACHIEVEMENTS[index].isACHIEVED = true;
												if (achSet.TYPE != AchievementType.WATCHADS)
												{
													DoubleReward(reward);
												}
												//MRUtilities.Instance.LogEvent("Achievements", "AchivementRewardClaimed", achSet.ACHIEVEMENTS[index].NAME);
												SFXManager.instance.PlaySound(10);
												this._this.StartCoroutine(this._this.CreateAchievementItems(0f));
											}
											else
											{
												GUIManager.Instance.OpenScreenExplicitly(ScreenName.CoinsShop);
											}
										});
								}
								if (achSet.TYPE == AchievementType.WATCHADS)
								{
									gameObject.transform.Find("Star1").gameObject.SetActive(false);
									gameObject.transform.Find("Star2").GetComponent<Image>().gameObject.SetActive(false);
									gameObject.transform.Find("Star3").GetComponent<Image>().gameObject.SetActive(false);
									gameObject.transform.Find("Button_Reward").GetChild(0).gameObject.SetActive(false);
									gameObject.transform.Find("Button_Reward").GetChild(1).gameObject
										.GetComponent<Text>().text = "Spawn level";
									gameObject.transform.Find("Button_Reward").GetChild(2).gameObject
										.GetComponent<Text>().text = (DataProvider.instance.gameData.currentBoxLevel + 2) + " plant";
								}
							}
						}
						finally
						{
							((IDisposable) this._locvar2).Dispose();
						}

						this._PC = -1;
						break;
				}

				return false;
			}

			public void DoubleReward(float reward)
			{
				this._this.doubleRewardPanel.SetActive(true);
				this._this.ShowButton();
				this._this.currentReward.text = "You earned: " + reward.ToShortHandString();
				this._this.doubleReward.text = (2 * reward).ToShortHandString();
				this._this.doubleRewardValue = (2 * reward);
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

		public static AchievementsManager instance;

		public List<AchievementSet> achievementSets;

		public GameObject achievementItemPrefab;

		public GameObject gridRoot;

		public Sprite rewardClaim;

		public Sprite rewardNotAvailble;

		public Sprite achievementSetComplete;

		public Sprite ribbon1;

		public Sprite ribbon2;

		public Sprite ribbon3;

		public Sprite starGrey;

		public Sprite starGold;

		public GameObject achievementsButton;
		[Header("Double Reward Panel")]
		public GameObject doubleRewardPanel;
		public Text currentReward;
		public Text doubleReward;
		public GameObject buttonBack;
		[HideInInspector] public float doubleRewardValue;

		private bool AdsEnabled;

		public int ACH_DATA_ITEMSBOUGHT
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_ITEMSBOUGHT"))
				{
					PlayerPrefs.SetInt("ACH_DATA_ITEMSBOUGHT", 0);
				}

				return PlayerPrefs.GetInt("ACH_DATA_ITEMSBOUGHT");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_ITEMSBOUGHT", value); }
		}

		public int ACH_DATA_MERGES
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_MERGES"))
				{
					PlayerPrefs.SetInt("ACH_DATA_MERGES", 0);
				}

				return PlayerPrefs.GetInt("ACH_DATA_MERGES");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_MERGES", value); }
		}

		public int ACH_DATA_CRATESOPENED
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_CRATESOPENED"))
				{
					PlayerPrefs.SetInt("ACH_DATA_CRATESOPENED", 0);
				}

				return PlayerPrefs.GetInt("ACH_DATA_CRATESOPENED");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_CRATESOPENED", value); }
		}

		public int ACH_DATA_MYSTERIESOPENED
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_MYSTERIESOPENED"))
				{
					PlayerPrefs.SetInt("ACH_DATA_MYSTERIESOPENED", 0);
				}

				return PlayerPrefs.GetInt("ACH_DATA_MYSTERIESOPENED");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_MYSTERIESOPENED", value); }
		}

		public int ACH_DATA_LEVELREACHED
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_LEVELREACHED"))
				{
					PlayerPrefs.SetInt("ACH_DATA_LEVELREACHED", 1);
				}

				return PlayerPrefs.GetInt("ACH_DATA_LEVELREACHED");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_LEVELREACHED", value); }
		}

		public int ACH_DATA_TAPS
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_TAPS"))
				{
					PlayerPrefs.SetInt("ACH_DATA_TAPS", 0);
				}

				return PlayerPrefs.GetInt("ACH_DATA_TAPS");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_TAPS", value); }
		}

		public int ACH_DATA_ITEMSUNLOCKED
		{
			get
			{
				if (!PlayerPrefs.HasKey("ACH_DATA_ITEMSUNLOCKED"))
				{
					PlayerPrefs.SetInt("ACH_DATA_ITEMSUNLOCKED", 1);
				}

				return PlayerPrefs.GetInt("ACH_DATA_ITEMSUNLOCKED");
			}
			set { PlayerPrefs.SetInt("ACH_DATA_ITEMSUNLOCKED", value); }
		}

		public int ACH_DATA_WATCHADS
		{
			get
			{
				return PlayerPrefs.GetInt("ACH_DATA_WATCHADS");
			}
			set
			{
				PlayerPrefs.SetInt("ACH_DATA_WATCHADS", value);
			}
		}

		private void Awake()
		{
			AchievementsManager.instance = this;

			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}

		private void Start()
		{
			base.StartCoroutine(this.CreateAchievementItems(0.5f));
		}

		public void CaterAchivementsButton()
		{
			if (LevelManager.instance.currentLevel.levelNo > 1)
			{
				this.achievementsButton.SetActive(true);
				int rewardsAvailable = this.GetRewardsAvailable();
				if (rewardsAvailable > 0)
				{
					this.achievementsButton.transform.GetChild(0).gameObject.SetActive(true);
					this.achievementsButton.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text =
						rewardsAvailable.ToString();
				}
				else
				{
					this.achievementsButton.transform.GetChild(0).gameObject.SetActive(false);
				}
			}
			else
			{
				this.achievementsButton.SetActive(false);
			}
		}

		public void AchivementsManagerWorker()
		{
			base.StartCoroutine(this.CreateAchievementItems(0f));
		}

		private IEnumerator CreateAchievementItems(float p_waitTime)
		{
			AchievementsManager._CreateAchievementItems_c__Iterator0 _CreateAchievementItems_c__Iterator =
				new AchievementsManager._CreateAchievementItems_c__Iterator0();
			_CreateAchievementItems_c__Iterator.p_waitTime = p_waitTime;
			_CreateAchievementItems_c__Iterator._this = this;
			return _CreateAchievementItems_c__Iterator;
		}

		private void SetProgress(Achievement p_ach, GameObject achievementObject)
		{
			if (p_ach.TYPE == AchievementType.BUY)
			{
				if ((float) this.ACH_DATA_ITEMSBOUGHT > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_ITEMSBOUGHT + " / " + p_ach.TARGET;
				}

				float fillAmount;
				if (this.ACH_DATA_ITEMSBOUGHT > 0)
				{
					fillAmount = (float) this.ACH_DATA_ITEMSBOUGHT / p_ach.TARGET;
				}
				else
				{
					fillAmount = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount;
			}
			else if (p_ach.TYPE == AchievementType.MERGE)
			{
				if ((float) this.ACH_DATA_MERGES > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_MERGES + " / " + p_ach.TARGET;
				}

				float fillAmount2;
				if (this.ACH_DATA_MERGES > 0)
				{
					fillAmount2 = (float) this.ACH_DATA_MERGES / p_ach.TARGET;
				}
				else
				{
					fillAmount2 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount2;
			}
			else if (p_ach.TYPE == AchievementType.OPENCRATE)
			{
				if ((float) this.ACH_DATA_CRATESOPENED > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_CRATESOPENED + " / " + p_ach.TARGET;
				}

				float fillAmount3;
				if (this.ACH_DATA_CRATESOPENED > 0)
				{
					fillAmount3 = (float) this.ACH_DATA_CRATESOPENED / p_ach.TARGET;
				}
				else
				{
					fillAmount3 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount3;
			}
			else if (p_ach.TYPE == AchievementType.OPENMYSTERY)
			{
				if ((float) this.ACH_DATA_MYSTERIESOPENED > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_MYSTERIESOPENED + " / " + p_ach.TARGET;
				}

				float fillAmount4;
				if (this.ACH_DATA_MYSTERIESOPENED > 0)
				{
					fillAmount4 = (float) this.ACH_DATA_MYSTERIESOPENED / p_ach.TARGET;
				}
				else
				{
					fillAmount4 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount4;
			}
			else if (p_ach.TYPE == AchievementType.REACH)
			{
				if ((float) this.ACH_DATA_LEVELREACHED > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_LEVELREACHED + " / " + p_ach.TARGET;
				}

				float fillAmount5;
				if (this.ACH_DATA_LEVELREACHED > 0)
				{
					fillAmount5 = (float) this.ACH_DATA_LEVELREACHED / p_ach.TARGET;
				}
				else
				{
					fillAmount5 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount5;
			}
			else if (p_ach.TYPE == AchievementType.TAP)
			{
				if ((float) this.ACH_DATA_TAPS > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_TAPS + " / " + p_ach.TARGET;
				}

				float fillAmount6;
				if (this.ACH_DATA_TAPS > 0)
				{
					fillAmount6 = (float) this.ACH_DATA_TAPS / p_ach.TARGET;
				}
				else
				{
					fillAmount6 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount6;
			}
			else if (p_ach.TYPE == AchievementType.UNLOCK)
			{
				if ((float) this.ACH_DATA_ITEMSUNLOCKED > p_ach.TARGET)
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_ITEMSUNLOCKED + " / " + p_ach.TARGET;
				}

				float fillAmount7;
				if (this.ACH_DATA_ITEMSUNLOCKED > 0)
				{
					fillAmount7 = (float) this.ACH_DATA_ITEMSUNLOCKED / p_ach.TARGET;
				}
				else
				{
					fillAmount7 = 0.1f / p_ach.TARGET;
				}

				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount7;
			}
			else if (p_ach.TYPE == AchievementType.WATCHADS)
			{
				if ((float) this.ACH_DATA_WATCHADS > p_ach.TARGET)  
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						p_ach.TARGET + " / " + p_ach.TARGET;
				}
				else
				{
					achievementObject.transform.Find("Text_Label_Progress").GetComponent<Text>().text =
						this.ACH_DATA_WATCHADS + " / " + p_ach.TARGET;
				}

				float fillAmount8;
				if (this.ACH_DATA_WATCHADS > 0)
				{
					fillAmount8 = (float) this.ACH_DATA_WATCHADS / p_ach.TARGET;
				}
				else
				{
					fillAmount8 = 0.1f / p_ach.TARGET;
				}
				 
				achievementObject.transform.Find("Image_ProgressFg").GetComponent<Image>().fillAmount = fillAmount8;
			}
		}

		private bool IsAchievementProgressCompleted(Achievement p_ach)
		{
			bool result = false;
			if (p_ach.TYPE == AchievementType.BUY)
			{
				if ((float) this.ACH_DATA_ITEMSBOUGHT >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.MERGE)
			{
				if ((float) this.ACH_DATA_MERGES >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.OPENCRATE)
			{
				if ((float) this.ACH_DATA_CRATESOPENED >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.OPENMYSTERY)
			{
				if ((float) this.ACH_DATA_MYSTERIESOPENED >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.REACH)
			{
				if ((float) this.ACH_DATA_LEVELREACHED >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.TAP)
			{
				if ((float) this.ACH_DATA_TAPS >= p_ach.TARGET)
				{
					result = true;
				}
			}
			else if (p_ach.TYPE == AchievementType.UNLOCK && (float) this.ACH_DATA_ITEMSUNLOCKED >= p_ach.TARGET)
			{
				result = true;
			}
			else if (p_ach.TYPE == AchievementType.WATCHADS)
			{
				if ((float) this.ACH_DATA_WATCHADS >= p_ach.TARGET)
				{
					result = true;
				}
			}

			return result;
		}

		public void DummyButton_IncrementBuy()
		{
			for (int i = 0; i < 450; i++)
			{
				this.IncrementAchievementEvent(AchievementType.BUY);
			}
		}

		public void DummyButton_IncrementMerge()
		{
			for (int i = 0; i < 5999; i++)
			{
				this.IncrementAchievementEvent(AchievementType.MERGE);
			}
		}

		public void DummyButton_IncrementOpenCrate()
		{
			for (int i = 0; i < 2999; i++)
			{
				this.IncrementAchievementEvent(AchievementType.OPENCRATE);
			}
		}

		public void DummyButton_IncrementOpenMystery()
		{
			for (int i = 0; i < 159; i++)
			{
				this.IncrementAchievementEvent(AchievementType.OPENMYSTERY);
			}
		}

		public void DummyButton_IncrementOpenReach()
		{
			for (int i = 0; i < 1; i++)
			{
				this.IncrementAchievementEvent(AchievementType.REACH);
			}
		}

		public void DummyButton_IncrementOpenTap()
		{
			for (int i = 0; i < 5999; i++)
			{
				this.IncrementAchievementEvent(AchievementType.TAP);
			}
		}

		public void DummyButton_IncrementOpenUnlock()
		{
			for (int i = 0; i < 1; i++)
			{
				this.IncrementAchievementEvent(AchievementType.UNLOCK);
			}
		}

		public void DummyButton_IncrementOpenWatchAds()
		{
			for (int i = 0; i < 1; i++)
			{
				this.IncrementAchievementEvent(AchievementType.WATCHADS);
			}
		}

		public void IncrementAchievementEvent(AchievementType p_achievementType)
		{
			if (p_achievementType == AchievementType.BUY)
			{
				this.ACH_DATA_ITEMSBOUGHT++;
			}
			else if (p_achievementType == AchievementType.MERGE)
			{
				this.ACH_DATA_MERGES++;
			}
			else if (p_achievementType == AchievementType.OPENCRATE)
			{
				this.ACH_DATA_CRATESOPENED++;
			}
			else if (p_achievementType == AchievementType.OPENMYSTERY)
			{
				this.ACH_DATA_MYSTERIESOPENED++;
			}
			else if (p_achievementType == AchievementType.REACH)
			{
				this.ACH_DATA_LEVELREACHED++;
			}
			else if (p_achievementType == AchievementType.TAP)
			{
				this.ACH_DATA_TAPS++;
			}
			else if (p_achievementType == AchievementType.UNLOCK)
			{
				this.ACH_DATA_ITEMSUNLOCKED++;
			}
			else if (p_achievementType == AchievementType.WATCHADS)
			{
				this.ACH_DATA_WATCHADS++;
			}

			int rewardsAvailable = this.GetRewardsAvailable();
			if (rewardsAvailable > 0)
			{
				this.achievementsButton.transform.GetChild(0).gameObject.SetActive(true);
				this.achievementsButton.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text =
					rewardsAvailable.ToString();
			}
			else
			{
				this.achievementsButton.transform.GetChild(0).gameObject.SetActive(false);
			}

			this.CaterAchivementsButton();
		}

		private int GetRewardsAvailable()
		{
			int num = 0;
			if (!this.achievementSets[2].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[2]
				    .ACHIEVEMENTS[this.achievementSets[2].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[2].ACHIEVEMENTS[this.achievementSets[2].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[2].ACHIEVEMENTS[this.achievementSets[2].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[2].ACHIEVEMENTS[this.achievementSets[2].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[2].ACHIEVEMENTS[this.achievementSets[2].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[3].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[3]
				    .ACHIEVEMENTS[this.achievementSets[3].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[3].ACHIEVEMENTS[this.achievementSets[3].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[3].ACHIEVEMENTS[this.achievementSets[3].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[3].ACHIEVEMENTS[this.achievementSets[3].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[3].ACHIEVEMENTS[this.achievementSets[3].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[5].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[5]
				    .ACHIEVEMENTS[this.achievementSets[5].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[5].ACHIEVEMENTS[this.achievementSets[5].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[5].ACHIEVEMENTS[this.achievementSets[5].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[5].ACHIEVEMENTS[this.achievementSets[5].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[5].ACHIEVEMENTS[this.achievementSets[5].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[6].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[6]
				    .ACHIEVEMENTS[this.achievementSets[6].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[6].ACHIEVEMENTS[this.achievementSets[6].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[6].ACHIEVEMENTS[this.achievementSets[6].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[6].ACHIEVEMENTS[this.achievementSets[6].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[6].ACHIEVEMENTS[this.achievementSets[6].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[1].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[1]
				    .ACHIEVEMENTS[this.achievementSets[1].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[1].ACHIEVEMENTS[this.achievementSets[1].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[1].ACHIEVEMENTS[this.achievementSets[1].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[1].ACHIEVEMENTS[this.achievementSets[1].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[1].ACHIEVEMENTS[this.achievementSets[1].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[4].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[4]
				    .ACHIEVEMENTS[this.achievementSets[4].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[4].ACHIEVEMENTS[this.achievementSets[4].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[4].ACHIEVEMENTS[this.achievementSets[4].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[4].ACHIEVEMENTS[this.achievementSets[4].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[4].ACHIEVEMENTS[this.achievementSets[4].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[0].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[0]
				    .ACHIEVEMENTS[this.achievementSets[0].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[0].ACHIEVEMENTS[this.achievementSets[0].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[0].ACHIEVEMENTS[this.achievementSets[0].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[0].ACHIEVEMENTS[this.achievementSets[0].CURRENTACHIEVEMENTINDEX].NAME);
					//MRUtilities.Instance.LogEvent("Achievements", "AchivementReported", this.achievementSets[0].ACHIEVEMENTS[this.achievementSets[0].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			if (!this.achievementSets[7].isACHIEVEMENTSETCOMPLETE &&
			    this.IsAchievementProgressCompleted(this.achievementSets[7]
				    .ACHIEVEMENTS[this.achievementSets[7].CURRENTACHIEVEMENTINDEX]))
			{
				num++;
				if (!this.achievementSets[7].ACHIEVEMENTS[this.achievementSets[7].CURRENTACHIEVEMENTINDEX].isREPORTED)
				{
					this.achievementSets[7].ACHIEVEMENTS[this.achievementSets[7].CURRENTACHIEVEMENTINDEX].isREPORTED =
						true;
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.AchievementUnlocked);
					GUIManager.Instance.CURRENTPANEL.GetComponent<Script_AchievementUnlocked>().SetAchivementName(
						this.achievementSets[7].ACHIEVEMENTS[this.achievementSets[7].CURRENTACHIEVEMENTINDEX].NAME);
				}
			}

			return num++;
		}

		public void WatchAdToGetDoubleReward()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.ShowRewardVideo("AchievementDoubleReward_REWARD");
					ads.instance.wasRequested = true;
					ads.instance.achievementDoubleReward = true;
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
				GetAchievementDoubleReward();
			}
		}

		public void GetAchievementDoubleReward()
		{
			DataProvider.instance.gameData.playerCoins += doubleRewardValue;
			doubleRewardPanel.SetActive(false);
			buttonBack.SetActive(false);
		}

		public void ShowButton()
		{
			StartCoroutine(ShowBackButton());
		}
		
		private IEnumerator ShowBackButton()
		{
			buttonBack.SetActive(false);
			yield return new WaitForSeconds(0.5f);
			buttonBack.SetActive(true);
		}
	}
}

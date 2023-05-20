using Mindravel.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager instance;

		public Text levelNo_Text;

		public GameObject LevelNoObject;

		public Text levelVal_Text;

		public Image levelVal_Image;

		public PlayerLevel[] playerLevels;

		public int currentLevelValue
		{
			get
			{
				return DataProvider.instance.gameData.playerLevelValue;
			}
			set
			{
				DataProvider.instance.gameData.playerLevelValue = value;
			}
		}

		public int currentLevelIndex
		{
			get
			{
				return DataProvider.instance.gameData.currentPlayerLevelIndex;
			}
			set
			{
				DataProvider.instance.gameData.currentPlayerLevelIndex = value;
			}
		}

		public PlayerLevel currentLevel
		{
			get
			{
				return this.playerLevels[this.currentLevelIndex];
			}
		}

		public bool LastLevel
		{
			get
			{
				return this.currentLevelIndex >= this.playerLevels.Length - 1;
			}
		}

		public void AnimateLevelNo()
		{
			this.LevelNoObject.GetComponent<TweenScale>().enabled = true;
		}

		private void UpdateLevelValUI()
		{
			if (this.currentLevel.levelNo == 35 && DataProvider.instance.gameData.playerLevelValue >= this.currentLevel.MaxRange)
			{
				this.levelVal_Text.text = "MAX";
			}
			else if (this.levelVal_Text != null)
			{
				this.levelVal_Text.text = this.currentLevelValue + "/" + this.currentLevel.MaxRange;
			}
			if (this.levelNo_Text != null)
			{
				this.levelNo_Text.text = this.currentLevel.levelNo.ToString();
			}
			if (this.levelVal_Image != null)
			{
				this.levelVal_Image.fillAmount = (float)this.currentLevelValue / (float)this.currentLevel.MaxRange;
			}
		}

		public void TestLevelInc()
		{
			this.IncLevelValue(this.currentLevel.MaxRange);
		}

		public void DoLevelCompleteAction()
		{
			if (this.currentLevel.levelNo == 8)
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.RateUs);
			}
		}

		public void IncLevelValue(int inc)
		{
			if (this.currentLevelValue + inc <= this.currentLevel.MaxRange)
			{
				this.currentLevelValue += inc;
			}
			else if (!this.LastLevel)
			{
				this.currentLevelValue = inc - (this.currentLevel.MaxRange - this.currentLevelValue);
				this.AnimateLevelNo();
				this.UpgradeLevel();
				this.DoLevelCompleteAction();
			}
			else
			{
				this.currentLevelValue = this.currentLevel.MaxRange;
			}
			if (this.currentLevel.levelNo == 5 && DataProvider.instance.gameData.playerLevelValue > this.currentLevel.MaxRange / 2 && !ShopManager.instance.firstTime_StarterPackSHOWN)
			{
				ShopManager.instance.UpdateShop();
			}
			this.UpdateLevelValUI();
		}

		public void UpgradeLevel()
		{
			Debug.Log("upgrade level");
			this.currentLevelIndex++;
			SFXManager.instance.PlaySound(Sound.LevelUp);
			AchievementsManager.instance.IncrementAchievementEvent(AchievementType.REACH);
			ShopManager.instance.UpdateShop();
			if (DataProvider.instance.gameData.maxGridSlots < 35)
			{
				//GridManager.instance.UnlockNewGridSlot();
				//GridManager.instance.AddPossibleSlot();
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				if (DataProvider.instance.gameData.maxGridSlots < 35)
				{
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("LEVEL UP", "CONGRATULATIONS!", string.Empty, delegate
					{
						GUIManager.Instance.Back();
						/*if (this.currentLevel.levelNo > 9 && !PlayerPrefs.HasKey("AutoFillTutorialShown"))
						{
							GUIManager.Instance.OpenScreenExplicitly(ScreenName.LandFillTUT);
							PlayerPrefs.SetInt("AutoFillTutorialShown", 1);
						}*/
					}, null, true);
					//MRUtilities.Instance.LogEvent("LevelUp", "LevelNumber", this.currentLevelIndex.ToString());
				}
			}
		}

		private void Awake()
		{
			LevelManager.instance = this;
            Application.targetFrameRate = 60;
		}

		private void Start()
		{
			/*if (this.currentLevel.levelNo > 9 && !PlayerPrefs.HasKey("AutoFillTutorialShown"))
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.LandFillTUT);
				PlayerPrefs.SetInt("AutoFillTutorialShown", 1);
			}*/
			this.UpdateLevelValUI();
		}
	}
}

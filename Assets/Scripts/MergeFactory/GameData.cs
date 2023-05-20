using System;
using UnityEngine;

namespace MergeFactory
{
	[CreateAssetMenu(menuName = "MergeFactory/Game Data")]
	public class GameData : ScriptableObject
	{
		[Header("TestValues")]
		public float _playerCoins;

		public int _playerCoinsMultiplier = 1;

		public int _currentBoxLevel;

		public int _highestUnitUnlocked;

		public int _boxIAPMultiplier = 1;

		public int _giftProbMultiplier = 1;

		public int _removeAds;

		[Header("XP Level")]
		public int _currentPlayerLevelIndex;

		public int _playerLevelValue = 1;

		[Header("Grid")]
		public int _maxGridSlots = 6;

		public int removeAds
		{
			get
			{
				return PlayerPrefs.GetInt("removeAds" + base.name, this._removeAds);
			}
			set
			{
				PlayerPrefs.SetInt("removeAds" + base.name, value);
			}
		}

		public float playerCoins
		{
			get
			{
				return PlayerPrefs.GetFloat("playerCoins" + base.name, this._playerCoins);
			}
			set
			{
				PlayerPrefs.SetFloat("playerCoins" + base.name, value);
			}
		}

		public int playerCoinsMultiplier
		{
			get
			{
				return PlayerPrefs.GetInt("playerCoinsMultiplier" + base.name, this._playerCoinsMultiplier);
			}
			set
			{
				PlayerPrefs.SetInt("playerCoinsMultiplier" + base.name, value);
			}
		}

		public int currentBoxLevel
		{
			get
			{
				return PlayerPrefs.GetInt("currentBoxLevel" + base.name, this._currentBoxLevel);
			}
			set
			{
				PlayerPrefs.SetInt("currentBoxLevel" + base.name, value);
			}
		}

		public int highestUnitUnlocked
		{
			get
			{
				return PlayerPrefs.GetInt("highestUnitUnlocked" + base.name, this._highestUnitUnlocked);
			}
			set
			{
				PlayerPrefs.SetInt("highestUnitUnlocked" + base.name, value);
			}
		}

		public int boxIAPMultiplier
		{
			get
			{
				return PlayerPrefs.GetInt("boxIAPMultiplier" + base.name, this._boxIAPMultiplier);
			}
			set
			{
				PlayerPrefs.SetInt("boxIAPMultiplier" + base.name, value);
			}
		}

		public int giftProbMultiplier
		{
			get
			{
				return PlayerPrefs.GetInt("giftProbMultiplier" + base.name, this._giftProbMultiplier);
			}
			set
			{
				PlayerPrefs.SetInt("giftProbMultiplier" + base.name, value);
			}
		}

		public int currentPlayerLevelIndex
		{
			get
			{
				return PlayerPrefs.GetInt("currentPlayerLevelIndex" + base.name, this._currentPlayerLevelIndex);
			}
			set
			{
				PlayerPrefs.SetInt("currentPlayerLevelIndex" + base.name, value);
			}
		}

		public int playerLevelValue
		{
			get
			{
				return PlayerPrefs.GetInt("playerLevelValue" + base.name, this._playerLevelValue);
			}
			set
			{
				PlayerPrefs.SetInt("playerLevelValue" + base.name, value);
			}
		}

		public int maxGridSlots
		{
			get
			{
				return PlayerPrefs.GetInt("maxGridSlots" + base.name, this._maxGridSlots);
			}
			set
			{
				PlayerPrefs.SetInt("maxGridSlots" + base.name, value);
			}
		}

		public int possibleGridSlots
		{
			get
			{
				return PlayerPrefs.GetInt("possibleGridSlots", 1);
			}
			set
			{
				PlayerPrefs.SetInt("possibleGridSlots", value);
			}
		}
		
		public float myCash
		{
			get
			{
				return PlayerPrefs.GetFloat("myCash", 0);
			}
			set
			{
				PlayerPrefs.SetFloat("myCash", value);
			}
		}

		public int orchidToken
		{
			get
			{
				return PlayerPrefs.GetInt("orchidToken", (highestUnitUnlocked * 500));
			}
			set
			{
				PlayerPrefs.SetInt("orchidToken", value);
			}
		}

		public int openedFortuneShrubCount
		{
			get
			{
				return PlayerPrefs.GetInt("openedFortuneShrubCount", 0);
			}
			set
			{
				PlayerPrefs.SetInt("openedFortuneShrubCount", value);
			}
		}

		public float BTC
		{
			get
			{
				return PlayerPrefs.GetFloat("BTC", 0);
			}
			set
			{
				PlayerPrefs.SetFloat("BTC", value);
			}
		}

		public int mergePlantCount
		{
			get
			{
				return PlayerPrefs.GetInt("mergePlantCount", 0);
			}
			set
			{
				PlayerPrefs.SetInt("mergePlantCount", value);
			}
		}

		public int openedMergePlantRewardPanelCount
		{
			get
			{
				return PlayerPrefs.GetInt("openedMergePlantRewardPanelCount", 0);
			}
			set
			{
				PlayerPrefs.SetInt("openedMergePlantRewardPanelCount", value);
			}
		}

		public int isStoreUnlockButtonActive
		{
			get
			{
				return PlayerPrefs.GetInt("isStoreUnlockButtonActive", 0);
			}
			set
			{
				PlayerPrefs.SetInt("isStoreUnlockButtonActive", value);
			}
		}
	}
}

using System;
using UnityEngine;

namespace MergeFactory
{
	[CreateAssetMenu(menuName = "MergeFactory/Unit")]
	public class Unit : ScriptableObject
	{
		public new string name;

		public int id = 1;

		public UnitStatus _status;

		[Space(10f)]
		public float rewardMoney = 1f;

		public float rewardTime = 2.5f;

		public float _price = 1f;

		[PreviewSprite, Space(5f)]
		public Sprite sprite;

		public int lvlText;

		public float cashValue;
		public int OTKValue;

		public UnitStatus status
		{
			get
			{
				this._status = (UnitStatus)PlayerPrefs.GetInt("unitStatus" + this.id, (int)this._status);
				return this._status;
			}
			set
			{
				this._status = value;
				PlayerPrefs.SetInt("unitStatus" + this.id, (int)this._status);
			}
		}

		public float rewardCoins
		{
			get
			{
				return this.rewardMoney * (float)DataProvider.instance.gameData.playerCoinsMultiplier;
			}
		}

		public float price
		{
			get
			{
				return PlayerPrefs.GetFloat("unitPrice" + this.id, this._price);
			}
			set
			{
				PlayerPrefs.SetFloat("unitPrice" + this.id, value);
			}
		}

		public float perSecondReward
		{
			get
			{
				return this.rewardCoins / this.rewardTime;
			}
		}

		public void BuyUnit()
		{
			this.price = this.price * 5180f / 4500f;
		}

		public void Reset()
		{
			if (this.id > 0)
			{
				this.status = UnitStatus.locked;
			}
		}
	}
}

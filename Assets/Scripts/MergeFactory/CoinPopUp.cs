using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class CoinPopUp : MonoBehaviour
	{
		public TweenAlpha alphaTween;

		public TweenPosition positionTween;

		public Text coinText;

		private Unit unit;

		public Vector3 _from = new Vector3(-30f, -20f, 0f);

		public Vector3 _to = new Vector3(-30f, 60f, 0f);

		public void SetCoins(Unit _unit)
		{
			this.unit = _unit;
			this.coinText.text = this.unit.rewardCoins.ToShortHandString();
		}

		public void SetPoition()
		{
			Vector3 to = this._to;
			Vector3 from = this._from;
			from.y *= CustomGridLayout.instance.CURRENT_BOXSIZE.x / 200f;
			to.y *= CustomGridLayout.instance.CURRENT_BOXSIZE.x / 200f;
			this.positionTween.to = to;
			this.positionTween.from = from;
		}

		public void SetObjectActive(bool val)
		{
			if (val)
			{
				this.SetPoition();
				SFXManager.instance.PlayCoinPopSound();
			}
			this.coinText.text = this.unit.rewardCoins.ToShortHandString();
			base.gameObject.SetActive(val);
			this.alphaTween.enabled = val;
			this.positionTween.enabled = val;
		}

		private void OnEnable()
		{
			this.SetPoition();
		}
	}
}

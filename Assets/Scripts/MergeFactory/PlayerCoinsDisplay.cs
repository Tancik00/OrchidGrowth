using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class PlayerCoinsDisplay : MonoBehaviour
	{
		public Display display;

		private Text coinsText;

		private void Start()
		{
			this.coinsText = base.GetComponent<Text>();
		}

		private void Update()
		{
			if (this.display == Display.totalCoins)
			{
				this.coinsText.text = DataProvider.instance.gameData.playerCoins.ToShortHandString();
			}
			else
			{
				this.coinsText.text = GridManager.instance.CoinsPerSec.ToShortHandString();
				if (DataProvider.instance.gameData.playerCoinsMultiplier > 1)
				{
					this.coinsText.color = Color.green;
				}
				else
				{
					this.coinsText.color = Color.white;
				}
			}
		}
	}
}

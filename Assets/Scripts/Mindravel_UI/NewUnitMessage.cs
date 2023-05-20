using MergeFactory;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mindravel.UI
{
	public class NewUnitMessage : MonoBehaviour
	{
		public Image unit_Image;

		public Text unitName_Text;
		public Text unitRewardText;
		public Text cashBalance;
		public Text titleText;
		public Text OTKCount;

		public void ShowMessage(Unit unit)
		{
			this.unit_Image.sprite = UnitManager.instance.itemUnlockedSprites[unit.id];
			this.unit_Image.SetNativeSize();
			this.unitName_Text.text = unit.name;
			titleText.text = "NEW PLANT DISCOVERED";
			unitRewardText.text = "Win $" + unit.cashValue;
			cashBalance.text = "Cash Balance: $" + DataProvider.instance.gameData.myCash.ToString("F2");
			OTKCount.text = unit.OTKValue + " OTK";
		}

		public void ShoeMessageForGettingLuckyShrub(Sprite sprite, string name)
		{
			unit_Image.sprite = sprite;
			this.unit_Image.SetNativeSize();
			unitName_Text.text = name;
			titleText.text = "LUCKY SHRUB DISCOVERED";
			unitRewardText.gameObject.SetActive(false);
			cashBalance.gameObject.SetActive(false);
		}
	}
}

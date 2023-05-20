using MergeFactory;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Script_ItemInfo : MonoBehaviour
{
	public Text text_label_itemName;

	public Text text_label_coinsPerSecond;

	public Text text_label_storePrice;

	public Image image_item;

	public GameObject informationPanel;

	public TweenAlpha tweenAlpha;

	public TweenScale tweenScale;

	private void OnEnable()
	{
		this.tweenAlpha.ResetToBeginning();
		this.tweenScale.ResetToBeginning();
		this.tweenAlpha.PlayForward();
		this.tweenScale.PlayForward();
		base.GetComponent<AudioSource>().Play();
	}

	public void SetInformation(Unit p_unit, float p_posY)
	{
		this.text_label_itemName.text = p_unit.name;
		this.text_label_coinsPerSecond.text = p_unit.rewardCoins.ToShortHandString();
		this.image_item.sprite = UnitManager.instance.itemUnlockedSprites[p_unit.id];
		if (p_unit.id <= DataProvider.instance.gameData.highestUnitUnlocked - 3)
		{
			this.text_label_storePrice.text = p_unit.price.ToShortHandString();
		}
		else
		{
			this.text_label_storePrice.text = "LOCKED";
		}
	}
}

using MergeFactory;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Script_CoinsShop : MonoBehaviour
{
	public Text text_label_instantCoinsAmount;

	public Text text_label_megaCoinsAmount;

	private void OnEnable()
	{
		this.UpdateCoinsShopUI();
	}

	public void Button_BuyInstantCoinsClicked()
	{
		ShopManager.instance.BuyCoins();
	}

	public void Button_BuyMegaCoinsClicked()
	{
		ShopManager.instance.BuyMegaCoins();
	}

	private void UpdateCoinsShopUI()
	{
		this.text_label_instantCoinsAmount.text = (UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 15f).ToShortHandString();
		this.text_label_megaCoinsAmount.text = (UnitManager.instance.units[UnitManager.instance.MaxItemID].price * 150f).ToShortHandString();
	}
}

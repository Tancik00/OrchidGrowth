using MergeFactory;
using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GiftboxPanel : MonoBehaviour
{
	public delegate void ConfirmationActions();

	private sealed class _AnimationRoutine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal GiftboxPanel _this;

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

		public _AnimationRoutine_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				//this._this.ShowAnimation();
				//SFXManager.instance.PlaySound(Sound.MystryBoxAnimation);
				//this._current = new WaitForSeconds(1f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._this.ShowItem();
				SFXManager.instance.PlaySound(Sound.MystryBoxItem);
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

	public Text title_Text;

	public Text unitName_Text;

	public Text message_Text;

	public Image unit_Image;

	public GameObject upgrade_Button;
	public GameObject buttonBack;

	public GameObject giftAnimation;

	public GameObject rays;

	public GameObject maxButton;

	private int unitID;

	private GiftboxPanel.ConfirmationActions confirmedAction;

	private GiftboxPanel.ConfirmationActions cancelAction;

	private bool AdsEnabled;
	private Text _upgradeTxt;

    private void Awake()
    {
		AdsEnabled = true;
		
#if UNITY_EDITOR
		AdsEnabled = false;
#endif
	}

    private void Start()
    {
	    _upgradeTxt = upgrade_Button.GetComponentInChildren<Text>();
    }

    private void OnEnable()
	{
		//this.ShowAnimation();
	}

	private void ShowItem()
	{
		this.unitName_Text.text = UnitManager.instance.units[this.unitID].name;
		Debug.Log("unit ID: " + this.unitID);
		this.unit_Image.sprite = UnitManager.instance.itemUnlockedSprites[this.unitID];
		this.unitName_Text.gameObject.SetActive(true);
		this.unit_Image.gameObject.SetActive(true);
		this.rays.SetActive(true);
		if (this.message_Text.text == string.Empty)
		{
			this.upgrade_Button.SetActive(false);
			if (this.unitID <= 34)
			{
				this.maxButton.SetActive(true);
			}
		}
		else
		{
			this.upgrade_Button.SetActive(true);
			this.maxButton.SetActive(false);
			_upgradeTxt.text = "Upgrade to level " + (unitID + 2);
		}
		//this.giftAnimation.SetActive(false);
		StartCoroutine(ShowBackButton());
	}

	private IEnumerator ShowBackButton()
	{
		yield return new WaitForSeconds(0.5f);
		buttonBack.SetActive(true);
	}

	private void ShowAnimation()
	{
		this.maxButton.SetActive(false);
		this.rays.SetActive(false);
		this.unitName_Text.gameObject.SetActive(false);
		this.unit_Image.gameObject.SetActive(false);
		//this.giftAnimation.SetActive(false);
		this.upgrade_Button.SetActive(false);
		//this.giftAnimation.SetActive(true);
	}

	public void Animate()
	{
		base.StartCoroutine(this.AnimationRoutine());
	}

	private IEnumerator AnimationRoutine()
	{
		GiftboxPanel._AnimationRoutine_c__Iterator0 _AnimationRoutine_c__Iterator = new GiftboxPanel._AnimationRoutine_c__Iterator0();
		_AnimationRoutine_c__Iterator._this = this;
		return _AnimationRoutine_c__Iterator;
	}

	public void ShowGift(string title, int _unitID, string message = "", GiftboxPanel.ConfirmationActions _confirmedAction = null, GiftboxPanel.ConfirmationActions _cancelAction = null)
	{
		this.title_Text.text = title;
		this.unitID = _unitID;
		base.StartCoroutine(this.AnimationRoutine());
		this.message_Text.text = message;
		this.confirmedAction = _confirmedAction;
		this.cancelAction = _cancelAction;
	}

	public void Button_Confirm()
	{
		if (AdsEnabled)
		{
			if (ads.instance.rewardedAvailable)
				ads.instance.ShowRewardVideo("UpgradeBox_REWARDED");
			else
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
			}
		}
		else
			UpgradeBoxReward();

	}

	public void Button_Cancel()
	{
		if (this.cancelAction != null)
		{
			this.cancelAction();
		}
		else
		{
			GUIManager.Instance.Back();
		}
	}

	public void UpgradeBoxReward()
    {
		if (this.confirmedAction != null)
		{
			this.confirmedAction();
		}
		else
		{
			GUIManager.Instance.Back();
		}
	}
}

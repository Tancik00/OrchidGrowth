using MergeFactory;
using Mindravel.UI;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class LandFillManager : MonoBehaviour
{
	public static LandFillManager Instance;

	public RectTransform landFillPanel;
	public int defaultFills;

	public Text fillsText;

	private bool AdsEnabled;
	private Vector3 _landFillPanelInitPosition;

//	private static MRUtilities.RewardedVideoAction __f__am_cache0;

	public int TotalFills
	{
		get
		{
			return PlayerPrefs.GetInt("TotalFills", this.defaultFills);
		}
		set
		{
			PlayerPrefs.SetInt("TotalFills", value);
		}
	}

	private void Awake()
	{
		LandFillManager.Instance = this;

		AdsEnabled = true;

#if UNITY_EDITOR
		AdsEnabled = false;
#endif
	}

	public void WatchVideoToFillLand()
	{
		if (AdsEnabled)
		{
			if (ads.instance.rewardedAvailable)
				ads.instance.ShowRewardVideo("Add10Land_REWARDED");
			else
			{
				GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
				GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);          
			}
		}
		else
			this.AddLandFills(10);

		//UnityEngine.Debug.Log("Watch Requested");
		//      if (AdsControl.Instance.GetRewardAvailable())
		//      {
		//          AdsControl.Instance.PlayDelegateRewardVideo(delegate
		//          {
		//              this.AddLandFills(10);
		//              // MRUtilities.Instance.LogEvent("VideoWatchedToGetLandFill");

		//              //FireBaseManager.Instance.LogScreen("VideoWatchedToGetLandFill");
		//          });
		//      }
		//      else
		//      {
		//          GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
		//          GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
		//          //FireBaseManager.Instance.LogScreen("VideoNotAvailableLandFill");
		//      }

		/*
		MRUtilities.Instance.ShowVideoAd(delegate
		{
			this.AddLandFills(10);
			MRUtilities.Instance.LogEvent("VideoWatchedToGetLandFill");
		}, delegate
		{
			GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
			GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
			MRUtilities.Instance.LogEvent("VideoNotAvailableLandFill");
		});
		*/

	}

	public void AddLandFills(int fills)
	{
		UnityEngine.Debug.Log("Adding Land Auto Fills = " + fills);
		this.TotalFills += fills;
		this.fillsText.text = this.TotalFills.ToString();
		//GUIManager.Instance.Back();
        //.Instance.LogEvent("AddedLandFills", "Amount", fills.ToString());
        //FireBaseManager.Instance.LogScreen("AddedLandFills");
    }

	public void FillLand()
	{
		if (this.TotalFills > 0)
		{
			if (GridManager.instance.hasFreeSlot())
			{
				this.TotalFills--;
				this.fillsText.text = this.TotalFills.ToString();
				BoxManager.instance.Button_TestSpawn();
                //MRUtilities.Instance.LogEvent("LandAutoFilled");
                //FireBaseManager.Instance.LogScreen("LandAutoFilled");
			}
		}
		else
		{
			GUIManager.Instance.OpenScreenExplicitly(ScreenName.AutoLandFill);
			landFillPanel.anchoredPosition = _landFillPanelInitPosition;
		}
	}

	private void Start()
	{
		this.fillsText.text = this.TotalFills.ToString();
		_landFillPanelInitPosition = landFillPanel.anchoredPosition;
	}
}

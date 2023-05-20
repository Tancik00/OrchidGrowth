using System;
using UnityEngine;

public class BasicAdNetwork : MonoBehaviour
{
	public virtual void RequestBanner()
	{
	}

	public virtual void DestroyBanner()
	{
	}

	public virtual void RequestInterstitial()
	{
	}

	public virtual void RequestVideoAd()
	{
	}

	public virtual void ShowInterstitialAd()
	{
	}

	public virtual void ShowVideoAd()
	{
	}

	public virtual void ShowForcedInterstitial()
	{
	}

	public virtual void ShowForcedVideoAd()
	{
	}

	public virtual bool IsInterstitialRequested()
	{
		return false;
	}

	public virtual bool IsInterstitialLoaded()
	{
		return false;
	}

	public virtual bool IsVideoAdRequested()
	{
		return false;
	}

	public virtual bool IsVideoAdLoaded()
	{
		return false;
	}
}

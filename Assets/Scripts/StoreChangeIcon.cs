using System;
using MergeFactory;
using UnityEngine;

public class StoreChangeIcon : MonoBehaviour
{
	public static StoreChangeIcon Instance;

	public GameObject storeChangeIcon;

	public bool storeItemStatus
	{
		get
		{
			return PlayerPrefs.HasKey("storeItemStatus");
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("storeItemStatus", 1);
			}
			else
			{
				PlayerPrefs.DeleteKey("storeItemStatus");
			}
		}
	}

	public void ShowStoreChangeIcon(bool val)
	{
		this.storeItemStatus = val;
		this.storeChangeIcon.SetActive(this.storeItemStatus);
	}

	private void Awake()
	{
		StoreChangeIcon.Instance = this;
	}

	private void Start()
	{
		UnityEngine.Debug.Log("storeItemStatus : " + this.storeItemStatus);
		this.ShowStoreChangeIcon(this.storeItemStatus);
	}

	public void SetUnlockButton()
	{
		UnitStore.instance.SetUnlockButton();
	}
}

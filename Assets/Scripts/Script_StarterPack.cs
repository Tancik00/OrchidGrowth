using System;
using UnityEngine;

public class Script_StarterPack : MonoBehaviour
{
	public GameObject starterPack;

	public GameObject megaPack;

	private void OnEnable()
	{
		this.starterPack.SetActive(true);
		this.megaPack.SetActive(false);
	}

	public void Button_TryMegaPackClicked()
	{
		this.starterPack.SetActive(false);
		this.megaPack.SetActive(true);
	}

	public void Button_TryStarterPackClicked()
	{
		this.starterPack.SetActive(true);
		this.megaPack.SetActive(false);
	}
}

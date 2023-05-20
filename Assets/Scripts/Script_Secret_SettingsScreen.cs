using MergeFactory;
using System;
using UnityEngine;

public class Script_Secret_SettingsScreen : MonoBehaviour
{
	private int topLeft;

	private int topRight;

	private int bottomLeft;

	private int bottomRight;

	private void OnEnable()
	{
		this.topLeft = 0;
		this.topRight = 0;
		this.bottomLeft = 0;
		this.bottomRight = 0;
	}

	public void Button_TopLeftClicked()
	{
		this.topLeft++;
	}

	public void Button_TopRightClicked()
	{
		this.topRight++;
	}

	public void Button_BottomLeftClicked()
	{
		this.bottomLeft++;
	}

	public void Button_BottomRightClicked()
	{
		this.bottomRight++;
	}

	private void Update()
	{
		if (this.topLeft == 6 && this.topRight == 13 && this.bottomRight == 7 && this.bottomLeft == 21)
		{
			DataProvider.instance.TestCurrency(1E+24f);
		}
	}
}

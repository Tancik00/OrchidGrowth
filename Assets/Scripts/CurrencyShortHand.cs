using System;
using UnityEngine;

public static class CurrencyShortHand
{
	private static float thousand = 1000f;

	private static float million = 1000000f;

	private static float billion = 1E+09f;

	private static float trillion = 1E+12f;

	private static float quadrillion = 1E+15f;

	private static float gazillion = 1E+18f;

	private static float xillion = 1E+21f;

	public static string ToTimeString(this double seconds)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
		return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
	}

	public static bool IsInt(float val)
	{
		return val == (float)Mathf.FloorToInt(val);
	}

	public static string ToShortHandString(this float currency)
	{
		if (currency < CurrencyShortHand.thousand)
		{
			if (CurrencyShortHand.IsInt(currency))
			{
				return Mathf.CeilToInt(currency).ToString();
			}
			return currency.ToString("F2");
		}
		else if (currency >= CurrencyShortHand.thousand && currency < CurrencyShortHand.million)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.thousand))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.thousand).ToString() + "K";
			}
			return (currency / CurrencyShortHand.thousand).ToString("F2") + "K";
		}
		else if (currency >= CurrencyShortHand.million && currency < CurrencyShortHand.billion)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.million))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.million).ToString() + "M";
			}
			return (currency / CurrencyShortHand.million).ToString("F2") + "M";
		}
		else if (currency >= CurrencyShortHand.billion && currency < CurrencyShortHand.trillion)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.billion))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.billion).ToString() + "B";
			}
			return (currency / CurrencyShortHand.billion).ToString("F2") + "B";
		}
		else if (currency >= CurrencyShortHand.trillion && currency < CurrencyShortHand.quadrillion)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.trillion))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.trillion).ToString() + "T";
			}
			return (currency / CurrencyShortHand.trillion).ToString("F2") + "T";
		}
		else if (currency >= CurrencyShortHand.quadrillion && currency < CurrencyShortHand.gazillion)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.quadrillion))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.quadrillion).ToString() + "Q";
			}
			return (currency / CurrencyShortHand.quadrillion).ToString("F2") + "Q";
		}
		else if (currency >= CurrencyShortHand.gazillion && currency < CurrencyShortHand.xillion)
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.gazillion))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.gazillion).ToString() + "G";
			}
			return (currency / CurrencyShortHand.gazillion).ToString("F2") + "G";
		}
		else
		{
			if (CurrencyShortHand.IsInt(currency / CurrencyShortHand.xillion))
			{
				return Mathf.CeilToInt(currency / CurrencyShortHand.xillion).ToString() + "X";
			}
			return (currency / CurrencyShortHand.xillion).ToString("F2") + "X";
		}
	}
}

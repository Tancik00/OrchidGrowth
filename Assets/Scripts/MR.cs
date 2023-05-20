using System;
using UnityEngine;
using UnityEngine.UI;

public static class MR
{
	private static int logCounter = 1;

	public static void Log(string p_text)
	{
		if (GameObject.Find("MRLogScrollView"))
		{
			string text = GameObject.Find("MRLogScrollView").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text;
			text += "\n";
			text += "----------------------------------";
			text += "\n";
			text = text + MR.logCounter++ + " - ";
			text += p_text;
			GameObject.Find("MRLogScrollView").transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = text;
		}
		UnityEngine.Debug.Log("<color=yellow>MRINFO - \"" + p_text + "\"</color>");
	}

	public static void LogError(string p_text)
	{
		if (GameObject.Find("MRLogScrollView"))
		{
		}
	}
}

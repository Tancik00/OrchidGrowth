using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class StarterPackManager : MonoBehaviour
	{
		public static StarterPackManager instance;

		public double hoursToShowFor = 24.0;

		public bool starterPackAvailable;

		public Text timeText;

		public Text timeText2;

		public GameObject starterPackButton;

		private DateTime targetDateTime;

		private double remainingSeconds;

		public bool StarterPackBought
		{
			get
			{
				return PlayerPrefs.HasKey("starterPackBought");
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("starterPackBought", 1);
				}
				else
				{
					PlayerPrefs.DeleteKey("starterPackBought");
				}
			}
		}

		private void Awake()
		{
			StarterPackManager.instance = this;
		}

		public void SaveStarterPackEndTime()
		{
			this.targetDateTime = DateTime.Now.AddHours(this.hoursToShowFor);
			PlayerPrefs.SetString("starterPackTime", this.targetDateTime.ToString());
		}

		public void UpdateStarterPack()
		{
			if (PlayerPrefs.HasKey("starterPackTime"))
			{
				this.targetDateTime = DateTime.Parse(PlayerPrefs.GetString("starterPackTime"));
			}
			else
			{
				this.targetDateTime = DateTime.Now.AddHours(this.hoursToShowFor);
			}
			this.remainingSeconds = (this.targetDateTime - DateTime.Now).TotalSeconds;
			if (this.remainingSeconds > 1.0)
			{
				this.starterPackAvailable = true;
			}
			else
			{
				this.starterPackButton.SetActive(false);
				this.starterPackAvailable = false;
			}
		}

		private void Update()
		{
			if (this.StarterPackBought)
			{
				return;
			}
			if (!this.starterPackAvailable)
			{
				return;
			}
			this.remainingSeconds -= (double)Time.deltaTime;
			this.timeText.text = this.remainingSeconds.ToTimeString();
			this.timeText2.text = this.timeText.text;
			if (this.remainingSeconds <= 0.0)
			{
				this.starterPackButton.SetActive(false);
				this.starterPackAvailable = false;
			}
		}
	}
}

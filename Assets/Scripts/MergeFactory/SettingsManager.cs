using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class SettingsManager : MonoBehaviour
	{
		public static SettingsManager instance;

		public Toggle localNotificationToggle;

		public GameObject soundON_Button;

		public GameObject soundOFF_Button;

		public GameObject musicON_Button;

		public GameObject musicOFF_Button;

		public GameObject vibrationON_Button;

		public GameObject vibrationOFF_Button;

		//public bool LOCALNOTIFICATION
		//{
		//	get
		//	{
		//		if (!PlayerPrefs.HasKey("LOCALNOTIFICATION"))
		//		{
		//			PlayerPrefs.SetInt("LOCALNOTIFICATION", 1);
		//		}
		//		return PlayerPrefs.GetInt("LOCALNOTIFICATION") == 1;
		//	}
		//	set
		//	{
		//		if (value)
		//		{
		//			PlayerPrefs.SetInt("LOCALNOTIFICATION", 1);
		//		}
		//		else
		//		{
		//			PlayerPrefs.SetInt("LOCALNOTIFICATION", 0);
		//		}
		//	}
		//}

		public bool SOUNDMUTE
		{
			get
			{
				return PlayerPrefs.HasKey("SOUNDMUTE");
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("SOUNDMUTE", 1);
				}
				else
				{
					PlayerPrefs.DeleteKey("SOUNDMUTE");
				}
			}
		}

		public bool MUSICMUTE
		{
			get
			{
				return PlayerPrefs.HasKey("MUSICMUTE");
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("MUSICMUTE", 1);
				}
				else
				{
					PlayerPrefs.DeleteKey("MUSICMUTE");
				}
			}
		}

		public bool VIBRATION
		{
			get
			{
				if (!PlayerPrefs.HasKey("VIBRATION"))
				{
					PlayerPrefs.SetInt("VIBRATION", 1);
				}
				return PlayerPrefs.GetInt("VIBRATION") == 1;
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("VIBRATION", 1);
				}
				else
				{
					PlayerPrefs.SetInt("VIBRATION", 0);
				}
			}
		}

		public void Button_MuteSound(bool val)
		{
			this.SOUNDMUTE = val;
			this.UpdateMuteUI();
		}

		public void Button_MuteMusic(bool val)
		{
			this.MUSICMUTE = val;
			this.UpdateMuteUI();
		}

		public void Button_SetVibration(bool val)
		{
			this.VIBRATION = val;
			this.UpdateMuteUI();
		}

		//public void Button_ToggleNotification()
		//{
		//	this.LOCALNOTIFICATION = this.localNotificationToggle.isOn;
		//	if (!this.LOCALNOTIFICATION)
		//	{
		//		MRNotificationsManager.Instance.ResetLocalNotification(20);
		//		MRNotificationsManager.Instance.ResetLocalNotification(21);
		//		MRNotificationsManager.Instance.ResetLocalNotification(22);
		//	}
		//}

		private void Awake()
		{
			SettingsManager.instance = this;
			//if (!PlayerPrefs.HasKey("SetLocalNotificationOnStart"))
			//{
			//	this.LOCALNOTIFICATION = true;
			//	PlayerPrefs.SetInt("SetLocalNotificationOnStart", 1);
			//}
			//this.localNotificationToggle.isOn = this.LOCALNOTIFICATION;
		}

		private void UpdateMuteUI()
		{
			this.soundOFF_Button.SetActive(this.SOUNDMUTE);
			this.soundON_Button.SetActive(!this.SOUNDMUTE);
			this.musicOFF_Button.SetActive(this.MUSICMUTE);
			this.musicON_Button.SetActive(!this.MUSICMUTE);
			SFXManager.instance.Mute(this.SOUNDMUTE);
			MusicManager.instance.Mute(this.MUSICMUTE);
			if (this.VIBRATION)
			{
				this.vibrationOFF_Button.SetActive(false);
				this.vibrationON_Button.SetActive(true);
			}
			else
			{
				this.vibrationOFF_Button.SetActive(true);
				this.vibrationON_Button.SetActive(false);
			}
		}

		private void Start()
		{
			this.UpdateMuteUI();
		}

		//public void Button_FacebookClicked()
		//{
		//	Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
		//}

		//public void Button_TwitterClicked()
		//{
		//	Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
		//}

		//public void Button_InstagramClicked()
		//{
		//	Application.OpenURL("https://www.facebook.com/FoxGameStudio1989/");
		//}
	}
}

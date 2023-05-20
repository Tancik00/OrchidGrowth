using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class GiftBoxTimer : MonoBehaviour
	{
		private sealed class _GiftBoxTimerWorker_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal GiftBoxTimer _this;

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

			public _GiftBoxTimerWorker_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					break;
				case 1u:
					break;
				case 2u:
					break;
				case 3u:
					break;
				default:
					return false;
				}
				IL_29:
				if (LevelManager.instance.currentLevel.levelNo >= 5)
				{
					if (this._this.giftAvailable)
					{
						this._this.giftBoxText.text = "FREE";
						this._this.giftBoxButton.interactable = true;
						this._current = new WaitForSeconds(1f);
						if (!this._disposing)
						{
							this._PC = 1;
						}
					}
					else
					{
						this._this.remaingSeconds -= 1.0;
						this._this.giftBoxText.text = this._this.remaingSeconds.ToTimeString();
						this._this.giftBoxButton.interactable = false;
						if (this._this.remaingSeconds <= 0.0)
						{
							this._this.giftAvailable = true;
							if (!StoreChangeIcon.Instance.storeChangeIcon.activeInHierarchy)
							{
								StoreChangeIcon.Instance.ShowStoreChangeIcon(true);
							}
						}
						this._current = new WaitForSeconds(1f);
						if (!this._disposing)
						{
							this._PC = 2;
						}
					}
				}
				else
				{
					this._current = new WaitForSeconds(1f);
					if (!this._disposing)
					{
						this._PC = 3;
					}
				}
				return true;
				goto IL_29;
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

		public static GiftBoxTimer instance;

		public double remaingSeconds;

		public bool giftAvailable = true;

		public Text giftBoxText;

		public Button giftBoxButton;

		private DateTime targetDateTime;

		private TimeSpan maxTime;

		private void Awake()
		{
			GiftBoxTimer.instance = this;
		}

		private void Start()
		{
			this.CheckTimer();
			base.StartCoroutine(this.GiftBoxTimerWorker());
		}

		public void CheckTimer()
		{
			if (PlayerPrefs.HasKey("giftboxTime"))
			{
				this.targetDateTime = DateTime.Parse(PlayerPrefs.GetString("giftboxTime"));
			}
			else
			{
				this.targetDateTime = DateTime.Now;
			}
			this.remaingSeconds = (this.targetDateTime - DateTime.Now).TotalSeconds;
			if (this.remaingSeconds > 0.0)
			{
				this.giftAvailable = false;
			}
			else
			{
				this.giftAvailable = true;
				if (!StoreChangeIcon.Instance.storeChangeIcon.activeInHierarchy && LevelManager.instance.currentLevel.levelNo >= 5)
				{
					StoreChangeIcon.Instance.ShowStoreChangeIcon(true);
				}
			}
		}

		public void ResetTimer()
		{
			this.targetDateTime = DateTime.Now.AddHours(1.0);
			PlayerPrefs.SetString("giftboxTime", this.targetDateTime.ToString());
			//MRNotificationsManager.Instance.SetLocalNotification(23);
			this.giftAvailable = false;
			this.CheckTimer();
		}

		public IEnumerator GiftBoxTimerWorker()
		{
			GiftBoxTimer._GiftBoxTimerWorker_c__Iterator0 _GiftBoxTimerWorker_c__Iterator = new GiftBoxTimer._GiftBoxTimerWorker_c__Iterator0();
			_GiftBoxTimerWorker_c__Iterator._this = this;
			return _GiftBoxTimerWorker_c__Iterator;
		}
	}
}

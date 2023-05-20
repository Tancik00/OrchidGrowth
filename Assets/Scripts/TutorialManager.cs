using MergeFactory;
using Mindravel.UI;
//using SA.Common.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TutorialManager : MonoBehaviour
{
	

	private sealed class _TUT_1_Routine_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal TutorialManager _this;

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

		public _TUT_1_Routine_c__Iterator0()
		{
		}

		

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			print("Move next num: " + num);
			

			switch (num)
			{
			case 0u:
				this._this.TUT_1_Inprogress = true;
				this._this.TUT1_PID = 0;
				this._this.BottomUI.SetActive(false);
				this._i___1 = 0;
				break;
			case 1u:
				this._this.Tut1_Panel[this._this.TUT1_PID++].SetActive(false);
				this._i___1++;
					print("Tut1_PID: " + this._this.TUT1_PID);
					break;
			case 2u:
				//this._this.TUT_1_COMPLETE = true;
				//MRUtilities.Instance.LogEvent("Tutorial1Completed");
				this._this.Tut1_Panel[this._this.TUT1_PID].SetActive(true);
				Time.timeScale = 0f;
					print("Tut2_PID: " + this._this.TUT1_PID);
					this._current = new WaitUntil(() => this._this.Tut1_Part[this._this.TUT1_PID]);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				this._this.Tut1_Panel[this._this.TUT1_PID].SetActive(false);
				this._this.StartCoroutine(this._this.RequestNotificationsPermissions());
				this._this.TUT_1_Inprogress = false;
				this._this.StartCoroutine(this._this.TUT_2_Routine());
				this._this.OpenPrizeTutorial();
				if (!this._disposing)
				{
					this._PC = 4;
				}
				return false;
			default:
				return false;
			}

			if (this._i___1 >= this._this.Tut1_Panel.Length - 1)
			{
				print("CHECK FREE SLOT!");
				this._this.BottomUI.SetActive(true);
				BoxManager.instance.FreeSlotCheck();
				this._current = new WaitUntil(() => this._this.newUnitDLGClosed);

				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			}
			this._this.Tut1_Panel[this._this.TUT1_PID].SetActive(true);
			this._current = new WaitUntil(() => this._this.Tut1_Part[this._this.TUT1_PID]);
			
			if (!this._disposing)
			{
				this._PC = 1;
				
			}
			return true;
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

		internal bool __m__0()
		{
			return this._this.Tut1_Part[this._this.TUT1_PID];
		}

		internal bool __m__1()
		{
			return this._this.newUnitDLGClosed;
		}

		internal bool __m__2()
		{
			return this._this.Tut1_Part[this._this.TUT1_PID];
		}
	}

	private sealed class _RequestNotificationsPermissions_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
	{
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

		public _RequestNotificationsPermissions_c__Iterator1()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(5f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				//Singleton<ISN_LocalNotificationsController>.Instance.RequestNotificationPermissions();
				this._PC = -1;
				break;
			}
			return false;
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

	private sealed class _TUT_2_Routine_c__Iterator2 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _i___1;

		internal TutorialManager _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		private static Func<bool> __f__am_cache0;

		private static Func<bool> __f__am_cache1;

		private static Func<bool> __f__am_cache2;

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

		public _TUT_2_Routine_c__Iterator2()
		{
		}
		

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._this.TUT_2_Inprogress = true;
				this._this.TUT2_PID = 0;
				this._current = new WaitUntil(() => DataProvider.instance.gameData.playerCoins >= 1000f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._current = new WaitUntil(() => GridManager.instance.hasFreeSlot());
				if (!this._disposing)
				{
					this._PC = 2;
				}
				return true;
			case 2u:
				this._current = new WaitUntil(() => GUIManager.Instance.CURRENTSCREENNAME == ScreenName.MainMenu);
				if (!this._disposing)
				{
					this._PC = 3;
				}
				return true;
			case 3u:
				this._i___1 = 0;
				break;
			case 4u:
				this._this.Tut2_Panel[this._this.TUT2_PID++].SetActive(false);
				this._i___1++;
				break;
			default:
				return false;
			}
			if (this._i___1 < this._this.Tut2_Panel.Length)
			{
				this._this.Tut2_Panel[this._this.TUT2_PID].SetActive(true);
				Time.timeScale = 0f;
				this._current = new WaitUntil(() => this._this.Tut2_Part[this._this.TUT2_PID]);
				if (!this._disposing)
				{
					this._PC = 4;
				}
				return true;
			}
			this._this.TUT_2_COMPLETE = true;
		//MRUtilities.Instance.LogEvent("Tutorial2Completed");
			this._this.TUT_2_Inprogress = false;
			return false;
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

		private static bool __m__0()
		{
			return DataProvider.instance.gameData.playerCoins >= 1000f;
		}

		private static bool __m__1()
		{
			return GridManager.instance.hasFreeSlot();
		}

		private static bool __m__2()
		{
			return GUIManager.Instance.CURRENTSCREENNAME == ScreenName.MainMenu;
		}

		internal bool __m__3()
		{
			return this._this.Tut2_Part[this._this.TUT2_PID];
		}
	}

	public static TutorialManager instance;

	public GameObject BottomUI;

	[Header("TUT Panels")]
	public GameObject prizesTut;
	public GameObject prizesTutBg;
	public GameObject raffleDrawTut;
	public GameObject prizesTutText;
	public GameObject raffleDrawTutText;
	public GameObject luckyShrubTut;
	public GameObject newLandTut;
	public GameObject dailyBonusTutorial;
	public int TUT1_PID;

	public GameObject[] Tut1_Panel;

	[Space(10f)]
	public int TUT2_PID;

	public GameObject[] Tut2_Panel;

	[Header("TUT Part Status")]
	public bool[] Tut1_Part;

	[Space(10f)]
	public bool[] Tut2_Part;

	public bool newUnitDLGClosed;

	public bool TUT_1_Inprogress;

	public bool TUT_2_Inprogress;

	public bool RunTut;

	public bool TUTCompleted
	{
		get
		{
			return this.TUT_1_COMPLETE && this.TUT_2_COMPLETE;
		}
	}

	public bool TUT_1_COMPLETE
	{
		get
		{
			return PlayerPrefs.HasKey("TUT_1_COMPLETE");
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("TUT_1_COMPLETE", 1);
			}
			else
			{
				PlayerPrefs.DeleteKey("TUT_1_COMPLETE");
			}
		}
	}

	public bool TUT_2_COMPLETE
	{
		get
		{
			return PlayerPrefs.HasKey("TUT_2_COMPLETE");
		}
		set
		{
			if (value)
			{
				PlayerPrefs.SetInt("TUT_2_COMPLETE", 1);
			}
			else
			{
				PlayerPrefs.DeleteKey("TUT_2_COMPLETE");
			}
		}
	}

	public void OpenPrizeTutorial()
	{

		prizesTut.SetActive(true);
		prizesTutBg.SetActive(true);
		prizesTutText.SetActive(true);
	}

	public void OpenRaffleDrawTutorial()
	{
		if (!TUT_1_COMPLETE)
		{
			raffleDrawTut.SetActive(true);
			prizesTutBg.SetActive(true);
			raffleDrawTutText.SetActive(true);
		}
	}

	public void OpenLuckyShrubTutorial()
	{
		if (!TUT_1_COMPLETE)
		{
			luckyShrubTut.SetActive(true);
		}
	}

	public void OpenNewLandTutorial()
	{
		if (!TUT_1_COMPLETE)
		{
			newLandTut.SetActive(true);
		}
	}

	public void OpenDailyBonusTutorial()
	{
		if (!TUT_1_COMPLETE)
		{
			dailyBonusTutorial.SetActive(true);
			TUT_1_COMPLETE = true;
		}
	}
	
	public void CloseNewUnitDLG()
	{
		if (!this.TUT_1_COMPLETE)
		{
			this.newUnitDLGClosed = true;
		}
	}

	private void Awake()
	{
		TutorialManager.instance = this;
	}

	private void Start()
	{
		for (int i = 0; i < this.Tut1_Panel.Length; i++)
		{
			this.Tut1_Panel[i].SetActive(false);
		}
		for (int j = 0; j < this.Tut2_Panel.Length; j++)
		{
			this.Tut2_Panel[j].SetActive(false);
		}
		if (!this.TUTCompleted)
		{
			this.StartTUT_1();
		}
	}

	private void StartTUT_1()
	{
		if (this.TUT_1_COMPLETE)
		{
			this.StartTUT_2();
			return;
		}
		DataProvider.instance.ResetData();
		base.StartCoroutine(this.TUT_1_Routine());
	}

	public void CompleteTask_TUT2()
	{
		Time.timeScale = 1f;
		this.Tut2_Part[this.TUT2_PID] = true;
	}

	public void CompleteTask_TUT1()
	{
		Time.timeScale = 1f;
		this.Tut1_Part[this.TUT1_PID] = true;
	}

	private IEnumerator TUT_1_Routine()
	{
		TutorialManager._TUT_1_Routine_c__Iterator0 _TUT_1_Routine_c__Iterator = new TutorialManager._TUT_1_Routine_c__Iterator0();
		_TUT_1_Routine_c__Iterator._this = this;
		return _TUT_1_Routine_c__Iterator;
	}

	private IEnumerator RequestNotificationsPermissions()
	{
		return new TutorialManager._RequestNotificationsPermissions_c__Iterator1();
	}

	private IEnumerator TUT_2_Routine()
	{
		TutorialManager._TUT_2_Routine_c__Iterator2 _TUT_2_Routine_c__Iterator = new TutorialManager._TUT_2_Routine_c__Iterator2();
		_TUT_2_Routine_c__Iterator._this = this;
		return _TUT_2_Routine_c__Iterator;
	}

	private void StartTUT_2()
	{
		if (this.TUT_2_COMPLETE)
		{
			return;
		}
		base.StartCoroutine(this.TUT_2_Routine());
	}
}

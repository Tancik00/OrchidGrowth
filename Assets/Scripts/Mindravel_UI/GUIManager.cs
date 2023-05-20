using MergeFactory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Mindravel.UI
{
	public class GUIManager : MonoBehaviour
	{
		private sealed class _OpenScreenExplicitly_c__AnonStorey0
		{
			internal ScreenName screenName;

			internal bool __m__0(GameObject asd)
			{
				return asd.GetComponent<Screen>().screenName == this.screenName;
			}
		}

		private sealed class _OpenScreen_c__AnonStorey1
		{
			internal GameObject screenToOpen;

			internal bool __m__0(GameObject p)
			{
				return p.GetComponent<Screen>().screenName == this.screenToOpen.GetComponent<Screen>().screenName;
			}
		}

		[HideInInspector] public float unitCashValue;
		public RectTransform coinsPanel;
		public RectTransform starterPackPanel;
		public List<GameObject> panels;
		public GameObject mergePlantRewardPanel;

		public Stack<GameObject> navPanel = new Stack<GameObject>();

		private static GUIManager _instance;

		public static ScreenName defaultScreenName;

		public GameObject itemInformationScreen;

		private static Predicate<GameObject> __f__am_cache0;

		private static Predicate<GameObject> __f__am_cache1;
		private bool AdsEnabled;
		private Vector3 _coinsPanelInitPosition;
		private Vector3 _starterPackPanelInitPosition;

		public static GUIManager Instance
		{
			get
			{
				return GUIManager._instance;
			}
		}

		public GameObject CURRENTPANEL
		{
			get
			{
				return this.navPanel.Peek();
			}
			set
			{
			}
		}

		public ScreenName CURRENTSCREENNAME
		{
			get
			{
				GameObject gameObject = this.navPanel.Peek();
				return gameObject.GetComponent<Screen>().screenName;
			}
		}

		private void Awake()
		{
			//IronSource.Agent.validateIntegration();

			if (UnityEngine.Object.FindObjectsOfType<GUIManager>().Length > 1)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			GUIManager._instance = this;
			this.panels = new List<GameObject>();
			Canvas[] array = UnityEngine.Object.FindObjectsOfType<Canvas>();
			for (int i = 0; i < array.Length; i++)
			{
				Canvas canvas = array[i];
				IEnumerator enumerator = canvas.transform.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						Transform transform = (Transform)enumerator.Current;
						if (transform.gameObject.GetComponent<Screen>())
						{
							this.panels.Add(transform.gameObject);
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
			
			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}

		private void Start()
		{
			this.StartGUI();
			_coinsPanelInitPosition = coinsPanel.anchoredPosition;
			_starterPackPanelInitPosition = starterPackPanel.anchoredPosition;
		}

		public void Back()
		{
			if (this.navPanel != null && this.navPanel.Count > 0)
			{
				GameObject gameObject = this.navPanel.Pop();
				gameObject.SetActive(false);
				this.showCurrentPanel();
			}
			else
			{
				UnityEngine.Debug.Log("MRERROR - nav panel is null or zero count in BACK");
			}
		}
		//}

		public void IncreaseCash()
        {
	        DataProvider.instance.gameData.myCash += unitCashValue;
	        WithdrawController.Instance.UpdateCashTextOnMainScreen();
        }

		private void hideCurrentPanel()
		{
			GameObject gameObject = this.navPanel.Peek();
			gameObject.SetActive(false);
		}

		private void clearStack()
		{
			foreach (GameObject current in this.navPanel)
			{
				current.SetActive(false);
			}
			this.navPanel.Clear();
		}

		private void showCurrentPanel()
		{
			if (this.navPanel != null && this.navPanel.Count > 0)
			{
				GameObject gameObject = this.navPanel.Peek();
				gameObject.SetActive(true);
			}
			else
			{
				UnityEngine.Debug.Log("MRERROR - nav panel is null or zero count in ShowCurrentPanel");
			}
		}

		public void OpenScreenExplicitly(ScreenName screenName)
		{
			GameObject gameObject = this.panels.Find((GameObject asd) => asd.GetComponent<Screen>().screenName == screenName);
			if (this.navPanel != null && this.navPanel.Count > 0)
			{
				if (this.navPanel.Peek().name != gameObject.name)
				{
					this.OpenScreen(gameObject);
				}
			}
			else
			{
				UnityEngine.Debug.Log("MRERROR - nav panel is null or zero count in OpenScreenExplicitly");
			}
		}

		public void OpenScreen(GameObject screenToOpen)
		{
			bool fullScreen = screenToOpen.GetComponent<Screen>().fullScreen;
			int index = this.panels.FindIndex((GameObject p) => p.GetComponent<Screen>().screenName == screenToOpen.GetComponent<Screen>().screenName);
			if (fullScreen)
			{
				this.CURRENTPANEL.SetActive(false);
			}
			this.navPanel.Push(this.panels[index]);
			this.showCurrentPanel();
		}

		private void StartGUI()
		{
			foreach (GameObject current in this.panels)
			{
				current.SetActive(false);
			}
			this.navPanel.Clear();
			GameObject gameObject = this.panels.Find((GameObject asd) => asd.GetComponent<Screen>().firstScreen);
			this.navPanel.Push(gameObject);
			if (GUIManager.defaultScreenName != gameObject.GetComponent<Screen>().screenName)
			{
				this.navPanel.Push(this.panels.Find((GameObject asd) => asd.GetComponent<Screen>().screenName == GUIManager.defaultScreenName));
			}
			this.showCurrentPanel();
		}

		public void ShowItemInformation(Unit p_unit, float p_posY)
		{
			this.itemInformationScreen.GetComponent<Script_ItemInfo>().SetInformation(p_unit, p_posY);
			this.itemInformationScreen.SetActive(true);
		}

		public void HideItemInformation()
		{
			this.itemInformationScreen.SetActive(false);
		}

		public void ShowMergePlantRewardPanel()
		{
			mergePlantRewardPanel.SetActive(true);
		}

		public void ClaimReward()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.ShowRewardVideo("ClaimMergePlant_REWARD");
					ads.instance.wasRequested = true;
					ads.instance.claimMergePlantReward = true;
				}
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				}
			}
			else
			{
				ClaimMergePlantReward();
			}
		}

		public void ClaimMergePlantReward()
		{
			DataProvider.instance.gameData.myCash += 0.25f;
			WithdrawController.Instance.UpdateCashTextOnMainScreen();
			mergePlantRewardPanel.SetActive(false);
		}

		public void HidePanel(RectTransform panel)
		{
			var pos = panel.anchoredPosition;
			pos.x -= 2000f;
			panel.anchoredPosition = pos;
		}

		public void ShowCoinsPanel()
		{
			coinsPanel.anchoredPosition = _coinsPanelInitPosition;
		}

		public void ShowStarterPackPanel()
		{
			starterPackPanel.anchoredPosition = _starterPackPanelInitPosition;
		}
	}
}

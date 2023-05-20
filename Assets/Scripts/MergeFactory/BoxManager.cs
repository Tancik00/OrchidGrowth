using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class BoxManager : MonoBehaviour
	{
		public static BoxManager instance;

		public GameObject buyNewSlotPanel;
		public Vector2 offset = new Vector2(0f, -10f);

		[Space(5f)]
		public float _boxSpawnInterval = 10f;

		public float _probabilityOFMystryBox = 0.2f;

		public float currentProbVal;

		[Space(5f)]
		public bool startTimer;

		[Space(10f)]
		public List<GameObject> boxes = new List<GameObject>();

		public GameObject freeVideoBox;

		public GameObject mystryBox;

		public GameObject giftBox;

		[Space(10f)]
		public GameObject boxTapHand;

		public Text time_Text;

		public GameObject fullText;

		public Image timebar_Image;

		public Sprite[] boxButton_Sprites;

		[Header("Remaing Time")]
		public float remainingTime = 10f;

		public float boxSpawnInterval
		{
			get
			{
				return this._boxSpawnInterval / (float)DataProvider.instance.gameData.boxIAPMultiplier;
			}
		}

		public float probabilityOFMystryBox
		{
			get
			{
				if (DataProvider.instance.gameData.giftProbMultiplier == 1)
				{
					if (LevelManager.instance.currentLevel.levelNo <= 3)
					{
						this._probabilityOFMystryBox = 0f;
					}
					else if (LevelManager.instance.currentLevel.levelNo <= 5)
					{
						this._probabilityOFMystryBox = 0.04f;
					}
					else if (LevelManager.instance.currentLevel.levelNo <= 10)
					{
						this._probabilityOFMystryBox = 0.08f;
					}
					else
					{
						this._probabilityOFMystryBox = 0.2f;
					}
				}
				else
				{
					this._probabilityOFMystryBox = 0.4f;
				}
				return this._probabilityOFMystryBox;
			}
		}

		public float probabilityOFNormalBox
		{
			get
			{
				return 1f - this.probabilityOFMystryBox;
			}
		}

		public bool CanUpgradeBox
		{
			get
			{
				return DataProvider.instance.gameData.currentBoxLevel < this.boxes.Count - 1;
			}
		}

		private void Awake()
		{
			BoxManager.instance = this;
		}

		private void Start()
		{
			this.timebar_Image.sprite = this.boxButton_Sprites[DataProvider.instance.gameData.currentBoxLevel];
			this.remainingTime = this.boxSpawnInterval;
			base.Invoke("FreeSlotCheck", 0.5f);
		}

		public void UpdateBoxSprite()
		{
			this.timebar_Image.sprite = this.boxButton_Sprites[DataProvider.instance.gameData.currentBoxLevel];
		}

		public void FreeSlotCheck()
		{
			if (GridManager.instance.hasFreeSlot())
			{
				this.startTimer = true;
				this.boxTapHand.SetActive(true);
				if (!this.time_Text.transform.parent.gameObject.activeInHierarchy)
				{
					this.time_Text.transform.parent.gameObject.SetActive(true);
				}
				if (this.fullText.activeInHierarchy)
				{
					this.fullText.SetActive(false);
				}
			}
			else
			{
				this.startTimer = false;
				this.fullText.SetActive(true);
				this.timebar_Image.fillAmount = 0f;
				this.remainingTime = this.boxSpawnInterval;
				this.time_Text.transform.parent.gameObject.SetActive(false);
			}
		}

		public void CreateGiftBox()
		{
			if (GridManager.instance.hasFreeSlot())
			{
				Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
				//MRUtilities.Instance.LogEvent("BoxDropped", "Type", "GiftBox");
				Transform component = UnityEngine.Object.Instantiate<GameObject>(this.giftBox).GetComponent<Transform>();
				component.SetParent(randomFreeSlot.transform);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.GetComponent<RectTransform>().anchoredPosition = this.offset;
				component.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
				randomFreeSlot.SaveData();
				this.FreeSlotCheck();
			}
		}

		public void CreateUnitBox(Unit unit, bool freeFromVideo = false)
		{
			if (GridManager.instance.hasFreeSlot())
			{
				Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
				Transform component;
				if (freeFromVideo)
				{
					component = UnityEngine.Object.Instantiate<GameObject>(this.freeVideoBox).GetComponent<Transform>();
					component.GetComponent<Box>().boxType = BoxType.FreeVideo;
					//MRUtilities.Instance.LogEvent("BoxDropped", "Type", "VideoBox");
				}
				else
				{
					component = UnityEngine.Object.Instantiate<GameObject>(this.boxes[0]).GetComponent<Transform>();
					component.GetComponent<Box>().boxType = BoxType.UnitSpawner;
				//MRUtilities.Instance.LogEvent("BoxDropped", "Type", "NormalBox0");
				}
				component.GetComponent<Box>().unit = unit;
				component.SetParent(randomFreeSlot.transform);
				component.GetComponent<RectTransform>().localScale = Vector3.one;
				component.GetComponent<RectTransform>().anchoredPosition = this.offset;
				component.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
				randomFreeSlot.slotData.unit = unit;
				randomFreeSlot.SaveData();
				this.FreeSlotCheck();
			}
		}

		private void SpawnBox(Slot slot)
		{
			this.currentProbVal = UnityEngine.Random.Range(0f, 1f);
			Transform component;
			if (this.currentProbVal < this.probabilityOFMystryBox)
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.mystryBox).GetComponent<Transform>();
				//MRUtilities.Instance.LogEvent("BoxDropped", "Type", "MysteryBox");
			}
			else
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.boxes[DataProvider.instance.gameData.currentBoxLevel]).GetComponent<Transform>();
				//MRUtilities.Instance.LogEvent("BoxDropped", "Type", "NormalBox" + DataProvider.instance.gameData.currentBoxLevel);
			}
			component.SetParent(slot.transform);
			component.GetComponent<RectTransform>().localScale = Vector3.one;
			//component.GetComponent<RectTransform>().anchoredPosition = this.offset;
			component.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			component.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
			slot.SaveData();
			this.FreeSlotCheck();
		}

		public void SpawnBox(Slot slot, BoxType type)
		{
			Transform component;
			if (type == BoxType.Gift)
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.giftBox).GetComponent<Transform>();
			}
			else if (type == BoxType.Mystry)
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.mystryBox).GetComponent<Transform>();
			}
			else if (type == BoxType.Normal)
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.boxes[slot.slotData.boxLevel]).GetComponent<Transform>();
			}
			else if (type == BoxType.FreeVideo)
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.freeVideoBox).GetComponent<Transform>();
				component.GetComponent<Box>().boxType = BoxType.FreeVideo;
				component.GetComponent<Box>().unit = slot.slotData.unit;
			}
			else
			{
				component = UnityEngine.Object.Instantiate<GameObject>(this.boxes[0]).GetComponent<Transform>();
				component.GetComponent<Box>().boxType = BoxType.UnitSpawner;
				component.GetComponent<Box>().unit = slot.slotData.unit;
			}
			component.SetParent(slot.transform);
			component.GetComponent<RectTransform>().localScale = Vector3.one;
			//component.GetComponent<RectTransform>().anchoredPosition = this.offset;
			component.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			var pos = component.GetComponent<RectTransform>().anchoredPosition;
			pos.y -= 10;
			component.GetComponent<RectTransform>().anchoredPosition = pos;
			component.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
			slot.SaveData();
			this.FreeSlotCheck();
		}

		public void FillLand(int slotsToFill)
		{
			for (int i = 0; i < slotsToFill; i++)
			{
				if (!GridManager.instance.hasFreeSlot())
				{
					return;
				}
				Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
				this.SpawnBox(randomFreeSlot);
				this.remainingTime = this.boxSpawnInterval;
				this.FreeSlotCheck();
			}
		}

		public void Button_TestSpawn()
		{
			while (GridManager.instance.hasFreeSlot())
			{
				Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
				this.SpawnBox(randomFreeSlot);
				this.remainingTime = this.boxSpawnInterval;
				this.FreeSlotCheck();
			}
		}

		public void UpgradeBox()
		{
			if (this.CanUpgradeBox)
			{
				DataProvider.instance.gameData.currentBoxLevel++;
			}
		}

		public void CreateBox_Button()
		{
			SFXManager.instance.PlaySound(Sound.BoxButtonTap);
		//MRUtilities.Instance.LogEvent("BoxButtonTapped");
			AchievementsManager.instance.IncrementAchievementEvent(AchievementType.TAP);
			if (TutorialManager.instance.TUT_1_Inprogress)
			{
				this.CreateBox();
				TutorialManager.instance.CompleteTask_TUT1();
			}
			if (this.startTimer)
			{
				this.remainingTime -= 1f;
			}

			if (GridManager.instance.maxGridSlots < 35)
			{
				if (!GridManager.instance.hasFreeSlot())
				{
					buyNewSlotPanel.SetActive(true);
				}	
			}
		}

		private void CreateBox()
		{
			if (GridManager.instance.hasFreeSlot())
			{
				Slot randomFreeSlot = GridManager.instance.GetRandomFreeSlot();
				this.SpawnBox(randomFreeSlot);
				this.remainingTime = this.boxSpawnInterval;
				this.FreeSlotCheck();
				DailyTasksController.instance.IncrementDailyTaskEvent(DailyTaskType.GETPLANTS);
			}
		}

		private void Update()
		{
			if (!TutorialManager.instance.TUT_1_COMPLETE)
			{
				return;
			}
			if (this.startTimer)
			{
				this.remainingTime -= Time.deltaTime;
				this.time_Text.text = Mathf.CeilToInt(this.remainingTime).ToString();
				this.timebar_Image.fillAmount = 1f - this.remainingTime / this.boxSpawnInterval;
				if (this.remainingTime <= 0f)
				{
					this.boxTapHand.SetActive(true);
					this.CreateBox();
				}
			}
		}
	}
}

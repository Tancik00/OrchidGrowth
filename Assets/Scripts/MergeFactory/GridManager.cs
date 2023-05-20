using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Mindravel.UI;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MergeFactory
{
	public class GridManager : MonoBehaviour
	{
		private sealed class _showParticles_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal Slot newSlot;

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

			public _showParticles_c__Iterator0()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					this._current = new WaitForSeconds(2f);
					if (!this._disposing)
					{
						this._PC = 1;
					}
					return true;
				case 1u:
					ParticleManager.instance.ShowNewLandPartilecs(this.newSlot.transform.position);
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

		private sealed class _CheckForUniqueItems_c__Iterator1 : IEnumerator, IDisposable, IEnumerator<object>
		{
			internal float p_waitTime;

			internal GridManager _this;

			internal object _current;

			internal bool _disposing;

			internal int _PC;

			private static Func<Unit, int> __f__am_cache0;

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

			public _CheckForUniqueItems_c__Iterator1()
			{
			}

			public bool MoveNext()
			{
				uint num = (uint)this._PC;
				this._PC = -1;
				switch (num)
				{
				case 0u:
					if (TutorialManager.instance.TUTCompleted)
					{
						this._this.continueWork = true;
						this._this.tempUnits.Clear();
						this._current = new WaitForSeconds(this.p_waitTime);
						if (!this._disposing)
						{
							this._PC = 1;
						}
						return true;
					}
					break;
				case 1u:
				{
					int num2 = 0;
					while (num2 < this._this.gridSlots.Count && this._this.continueWork)
					{
						if (this._this.tempUnits.Contains(this._this.gridSlots[num2].slotData.unit))
						{
							this._this.continueWork = false;
						}
						if (this._this.gridSlots[num2].slotData.type == SlotType.Empty)
						{
							this._this.continueWork = false;
						}
						if (this._this.gridSlots[num2].type == SlotType.Unit)
						{
							this._this.tempUnits.Add(this._this.gridSlots[num2].slotData.unit);
						}
						num2++;
					}
					if (this._this.continueWork)
					{
						this._this.tempUnits = (from asd in this._this.tempUnits
						orderby asd.id
						select asd).ToList<Unit>();
						if (this._this.tempUnits != null && this._this.tempUnits.Count >= 2)
						{
							int index = this._this.gridSlots.FindIndex((Slot asd) => asd.slotData.unit == this._this.tempUnits[0]);
							int index2 = this._this.gridSlots.FindIndex((Slot asd) => asd.slotData.unit == this._this.tempUnits[1]);
							UnityEngine.Object.Destroy(this._this.gridSlots[index].item);
							UnitManager.instance.SpawnUnit(this._this.gridSlots[index], this._this.gridSlots[index2].slotData.unit.id);
							//MRUtilities.Instance.LogEvent("GameStuckUniqueItemsCatered");
						}
					}
					break;
				}
				default:
					return false;
				}
				this._PC = -1;
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

			private static int __m__0(Unit asd)
			{
				return asd.id;
			}

			internal bool __m__1(Slot asd)
			{
				return asd.slotData.unit == this._this.tempUnits[0];
			}

			internal bool __m__2(Slot asd)
			{
				return asd.slotData.unit == this._this.tempUnits[1];
			}
		}

		public GameObject possibleSlot;
		
		private static GridManager _instance;
		public static GridManager instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<GridManager>();
				}

				return _instance;
			}
		}

		public Slot slotNotFree;

		public List<Slot> gridSlots;

		public List<Slot> slots;

		private float minDistance;

		private int currentSlotIndex;

		private List<float> distances = new List<float>();

		private List<Slot> freeSlots = new List<Slot>();

		public InputField input_gridSlotNumber;

		private List<Unit> tempUnits = new List<Unit>();

		private bool continueWork = true;
		private bool AdsEnabled;

		public int maxGridSlots
		{
			get
			{
				return DataProvider.instance.gameData.maxGridSlots;
			}
			set
			{
				DataProvider.instance.gameData.maxGridSlots = value;
			}
		}

		public int FREESLOTS
		{
			get
			{
				int num = 0;
				for (int i = 0; i < DataProvider.instance.gameData.maxGridSlots; i++)
				{
					if (this.gridSlots[i].type == SlotType.Empty)
					{
						num++;
					}
				}
				return num;
			}
		}

		public float CoinsPerSec
		{
			get
			{
				float num = 0f;
				for (int i = 0; i < this.slots.Count; i++)
				{
					if (this.slots[i].type == SlotType.Unit)
					{
						num += this.slots[i].slotData.unit.perSecondReward;
					}
				}
				return num + DragableUnit.itemDragedPerSecondReward;
			}
		}

		public void DeleteItemFromGrid()
		{
			for (int i = 1; i < 15; i++)
			{
				int num = i;
				num--;
				UnityEngine.Object.Destroy(this.gridSlots[num].item);
				this.gridSlots[num].SaveData();
				BoxManager.instance.FreeSlotCheck();
			}
		}

		private void Awake()
		{
			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}

		public void ReplaceUpdatedItems()
		{
			for (int i = 0; i < DataProvider.instance.gameData.maxGridSlots; i++)
			{
				Slot slot = this.gridSlots[i];
				if (slot.slotData.type == SlotType.Box)
				{
					if (slot.slotData.boxType == BoxType.Normal && slot.slotData.boxLevel < DataProvider.instance.gameData.currentBoxLevel)
					{
						if (slot.item == null)
						{
							slot.slotData.boxLevel = DataProvider.instance.gameData.currentBoxLevel;
						}
						else
						{
							UnityEngine.Object.Destroy(slot.item);
							slot.slotData.boxLevel = DataProvider.instance.gameData.currentBoxLevel;
							BoxManager.instance.SpawnBox(slot, BoxType.Normal);
						}
					}
				}
				else if (slot.slotData.type == SlotType.Unit && slot.slotData.unit.id < DataProvider.instance.gameData.currentBoxLevel)
				{
					if (slot.item == null)
					{
						slot.slotData.unit = UnitManager.instance.units[DataProvider.instance.gameData.currentBoxLevel];
					}
					else
					{
						UnityEngine.Object.Destroy(slot.item);
						UnitManager.instance.SpawnUnit(slot, DataProvider.instance.gameData.currentBoxLevel);
					}
				}
			}
		}

		public void PopulateGrid()
		{
			this.slots.Clear();
			for (int i = 0; i < this.maxGridSlots; i++)
			{
				this.slots.Add(this.gridSlots[i]);
			}
			this.ReplaceUpdatedItems();
			for (int j = 0; j < this.slots.Count; j++)
			{
				this.slots[j].gameObject.SetActive(true);
			}
		}

		private void AdjustGridElements()
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				if (this.slots[i].item != null)
				{
					this.slots[i].item.SendMessage("AdjustSize", SendMessageOptions.DontRequireReceiver);
				}
			}
		}

		/*public void AddPossibleSlot()
		{
			if (this.maxGridSlots >= 40)
			{
				return;
			}

			DataProvider.instance.gameData.possibleGridSlots++;
			//this.possibleSlots[0].SetActive(true);
			for (int i = 0; i < DataProvider.instance.gameData.possibleGridSlots; i++)
			{
				//this.possibleSlots[this.maxGridSlots].SetActive(true);
				possibleSlot.SetActive(true);
			}
		}*/

		private int _possibleSlotID;
		public void UnlockNewGridSlot()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
				{
					ads.instance.ShowRewardVideo("AddSlot_REWARDED");
					ads.instance.wasRequested = true;
					ads.instance.addASlot = true;
				}
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				}
			}
			else
			{
				AddSlot();
			}
		}

		public void AddSlot()
		{
			if (this.maxGridSlots >= 35)
			{
				return;
			}
			/*for (int i = 0; i < possibleSlots.Count; i++)
			{
				possibleSlots[_possibleSlotID].SetActive(false);
			}*/
			this.gridSlots[this.maxGridSlots].gameObject.SetActive(true);
			this.maxGridSlots++;
			this.AdjustGridElements();
			if (maxGridSlots >= 35)
			{
				possibleSlot.SetActive(false);
				//DataProvider.instance.gameData.possibleGridSlots--;
			}
		}

		public void GetButtonID(int buttonID)
		{
			_possibleSlotID = buttonID;
		}

		private IEnumerator showParticles(Slot newSlot)
		{
			GridManager._showParticles_c__Iterator0 _showParticles_c__Iterator = new GridManager._showParticles_c__Iterator0();
			_showParticles_c__Iterator.newSlot = newSlot;
			return _showParticles_c__Iterator;
		}

		private void Start()
		{
			this.PopulateGrid();
			base.StartCoroutine(this.CheckForUniqueItems(0.4f));
			if (maxGridSlots >= 35)
			{
				possibleSlot.SetActive(false);
			}
			/*if (DataProvider.instance.gameData.possibleGridSlots <= 0)
			{
				possibleSlot.SetActive(false);
			}*/
			/*for (int i = 0; i < DataProvider.instance.gameData.possibleGridSlots; i++)
			{
				possibleSlots[i].SetActive(true);
			}*/
		}

		private bool _isEqualsUnitChecking = true;
		public List<int> allUnits;
		public List<int> equalsUnits;
		public List<int> unitIds;
		/*private void Update()
		{
			if (_isEqualsUnitChecking)
			{
				AutoMerge();
			}
		}

		private void AutoMerge()
		{
			for (int i = 0; i < slots.Count; i++)
			{
				if (slots[i].type == SlotType.Unit)
				{
					Debug.Log("=== " + slots[i].item.GetComponent<DragableUnit>().unit.id);
					allUnits.Add(slots[i].item.GetComponent<DragableUnit>().unit.id);
				}
			}

			for (int z = 0; z < allUnits.Count; z++)
			{
				for (int j = 1; j < allUnits.Count; j++)
				{
					if (allUnits[z] == allUnits[j] && z != j)
					{
						Debug.Log("= " + allUnits[z]);
						equalsUnits.Add(allUnits[z]);
						break;
					}
				}
			}
			
			RemoveRepeatableIds();

			_isEqualsUnitChecking = false;
		}
		
		private void RemoveRepeatableIds()
		{
			foreach (int item in GridManager.instance.equalsUnits.Distinct())
			{
				Debug.Log("== " + item);
				unitIds.Add(item);
			}
		}*/

		public IEnumerator CheckForUniqueItems(float p_waitTime)
		{
			GridManager._CheckForUniqueItems_c__Iterator1 _CheckForUniqueItems_c__Iterator = new GridManager._CheckForUniqueItems_c__Iterator1();
			_CheckForUniqueItems_c__Iterator.p_waitTime = p_waitTime;
			_CheckForUniqueItems_c__Iterator._this = this;
			return _CheckForUniqueItems_c__Iterator;
		}

		public void ResetGrid()
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				this.slots[i].ShowDestination(false);
				this.slots[i].ShowHighlight(false);
			}
		}

		public Slot GetRandomFreeSlot()
		{
			this.freeSlots.Clear();
			for (int i = 0; i < this.slots.Count; i++)
			{
				if (this.slots[i].type == SlotType.Empty && this.slots[i] != this.slotNotFree)
				{
					this.freeSlots.Add(this.slots[i]);
				}
			}
			return this.freeSlots[UnityEngine.Random.Range(0, this.freeSlots.Count)];
		}

		public void HighlightTargets(int id, Slot currentSlot)
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				if (this.slots[i].type == SlotType.Unit)
				{
					if (this.slots[i].item.GetComponent<DragableUnit>().unit.id == id && this.slots[i] != currentSlot)
					{
						this.slots[i].ShowHighlight(true);
					}
					else
					{
						this.slots[i].ShowHighlight(false);
					}
				}
			}
		}

		public bool hasFreeSlot()
		{
			for (int i = 0; i < this.slots.Count; i++)
			{
				if (this.slots[i].item == null && this.slots[i] != this.slotNotFree)
				{
					return true;
				}
			}
			return false;
		}

		public Slot GetDestination(Vector3 pos)
		{
			this.distances.Clear();
			for (int i = 0; i < this.slots.Count; i++)
			{
				float item = Vector3.Distance(pos, this.slots[i].transform.position);
				this.distances.Add(item);
			}
			this.minDistance = Mathf.Min(this.distances.ToArray());
			this.currentSlotIndex = 0;
			for (int j = 0; j < this.slots.Count; j++)
			{
				if (this.distances[j] == this.minDistance)
				{
					this.currentSlotIndex = j;
					this.slots[j].ShowDestination(true);
				}
				else
				{
					this.slots[j].ShowDestination(false);
				}
			}
			return this.slots[this.currentSlotIndex];
		}
	}
}

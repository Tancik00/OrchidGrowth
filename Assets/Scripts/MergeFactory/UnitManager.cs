using System;
using System.Collections.Generic;
using UnityEngine;

namespace MergeFactory
{
	public class UnitManager : MonoBehaviour
	{
		public static UnitManager instance;

		public List<Unit> units;

		public List<Sprite> itemUnlockedSprites;

		public int unitID;

		public GameObject unitPrefab;

		private GameObject currentUnit;

		public Vector2 offset;
		public bool isUnlockPlantButtonActive;
		private float _freeButtonAppearanceTime;

		public int MinItemID
		{
			get
			{
				return this.units[DataProvider.instance.gameData.currentBoxLevel + 1].id;
			}
		}

		public int MaxItemID
		{
			get
			{
				if (DataProvider.instance.gameData.highestUnitUnlocked - 4 > this.MinItemID)
				{
					return this.units[DataProvider.instance.gameData.highestUnitUnlocked - 4].id;
				}
				return this.MinItemID;
			}
		}

		public Unit MaxUnit
		{
			get
			{
				return this.units[this.MaxItemID];
			}
		}

		private void Awake()
		{
			UnitManager.instance = this;
			_freeButtonAppearanceTime = 60f;
		}

		private void Update()
		{
			_freeButtonAppearanceTime -= Time.deltaTime;
			if (_freeButtonAppearanceTime <= 0)
			{
				isUnlockPlantButtonActive = true;
				_freeButtonAppearanceTime = 60f;
			}
		}

		public void SpawnUnit(Slot slot, int _unitID)
		{
			this.currentUnit = UnityEngine.Object.Instantiate<GameObject>(this.unitPrefab);
			slot.slotData.unit = this.units[_unitID];
			slot.slotData.type = SlotType.Unit;
			DragableUnit component = this.currentUnit.GetComponent<DragableUnit>();
			component.unit = this.units[_unitID];
			this.currentUnit.transform.SetParent(slot.transform);
			this.currentUnit.GetComponent<RectTransform>().localScale = Vector3.one;
			this.currentUnit.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_BOXSIZE;
			this.currentUnit.transform.localPosition = Vector3.zero;
			this.currentUnit.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			component.image.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_ELEMENTSIZE;
			component.image.GetComponent<RectTransform>().SetPivot(PivotPresets.BottomCenter);
			component.image.GetComponent<RectTransform>().SetAnchor(AnchorPresets.BottonCenter, 0, 0);
			component.image.GetComponent<RectTransform>().anchoredPosition = this.offset;
			component.UpdateUnit();
			component.StartReward();
			component.coinPopUp.SetPoition();
		}
	}
}

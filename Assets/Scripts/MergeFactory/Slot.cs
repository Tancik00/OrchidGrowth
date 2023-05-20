using System;
using UnityEngine;

namespace MergeFactory
{
	public class Slot : MonoBehaviour
	{
		public SlotData slotData;

		public SlotType type
		{
			get
			{
				if (this.item == null)
				{
					this.slotData.type = SlotType.Empty;
					return SlotType.Empty;
				}
				if (this.item.GetComponentInParent<Box>())
				{
					this.slotData.type = SlotType.Box;
					return SlotType.Box;
				}

				if (this.item.GetComponentInParent<GridBubbleController>())
				{
					this.slotData.type = SlotType.Bubble;
					return SlotType.Bubble;
				}
				this.slotData.type = SlotType.Unit;
				this.slotData.unit = this.item.GetComponent<DragableUnit>().unit;
				return SlotType.Unit;
			}
		}

		public GameObject destinationMarker
		{
			get
			{
				if (base.transform.childCount > 0)
				{
					return base.transform.GetChild(0).gameObject;
				}
				return null;
			}
		}

		public GameObject highlightMarker
		{
			get
			{
				if (base.transform.childCount > 1)
				{
					return base.transform.GetChild(1).gameObject;
				}
				return null;
			}
		}

		public GameObject item
		{
			get
			{
				if (base.transform.childCount > 2)
				{
					return base.transform.GetChild(2).gameObject;
				}
				return null;
			}
		}

		public void ShowHighlight(bool show)
		{
			if (this.highlightMarker != null)
			{
				this.highlightMarker.SetActive(show);
			}
		}

		public void ShowDestination(bool show)
		{
			if (this.destinationMarker != null)
			{
				this.destinationMarker.SetActive(show);
			}
		}

		[ContextMenu("SaveData")]
		public void SaveData()
		{
			this.slotData.type = this.type;
			if (this.type == SlotType.Box)
			{
				this.slotData.boxType = this.item.GetComponent<Box>().boxType;
				this.slotData.boxLevel = this.item.GetComponent<Box>().boxlevel;
			}
			else if (this.type == SlotType.Unit)
			{
				this.slotData.unit = this.item.GetComponent<DragableUnit>().unit;
			}
			else
			{
				this.slotData.boxType = BoxType.Normal;
			}
		}

		private void LoadCurrentSlotData()
		{
			if (this.slotData == null)
			{
				return;
			}
			if (this.slotData.type == SlotType.Empty)
			{
				return;
			}
			if (this.slotData.type == SlotType.Box)
			{
				BoxManager.instance.SpawnBox(this, this.slotData.boxType);
			}
			else
			{
				UnitManager.instance.SpawnUnit(this, this.slotData.unit.id);
			}
		}

		private void OnEnable()
		{
			if (GridManager.instance == null)
			{
				print("Grid is null");
			}
			if (GridManager.instance != null && !GridManager.instance.slots.Contains(this))
			{
				GridManager.instance.slots.Add(this);
			}
			this.ShowHighlight(false);
			this.ShowDestination(false);
		}

		private void Start()
		{
			if (TutorialManager.instance.TUT_1_COMPLETE)
			{
				this.LoadCurrentSlotData();
			}
		}
	}
}

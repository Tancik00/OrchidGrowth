using System;
using UnityEngine;

namespace MergeFactory
{
	[CreateAssetMenu(menuName = "MergeFactory/Slot")]
	public class SlotData : ScriptableObject
	{
		public SlotType _type;

		public BoxType _boxType;

		[Header("Test Values")]
		public int TUT_UID = -1;

		public SlotType TUT_Type;

		public SlotType type
		{
			get
			{
				if (PlayerPrefs.HasKey("slotType" + base.name))
				{
					int @int = PlayerPrefs.GetInt("slotType" + base.name);
					if (@int == 0)
					{
						this._type = SlotType.Empty;
					}
					else if (@int == 1)
					{
						this._type = SlotType.Unit;
					}
					else
					{
						this._type = SlotType.Box;
					}
				}
				else
				{
					this.type = this.TUT_Type;
				}
				return this._type;
			}
			set
			{
				this._type = value;
				if (this._type == SlotType.Empty)
				{
					PlayerPrefs.SetInt("slotType" + base.name, 0);
				}
				else if (this._type == SlotType.Unit)
				{
					PlayerPrefs.SetInt("slotType" + base.name, 1);
				}
				else
				{
					PlayerPrefs.SetInt("slotType" + base.name, 2);
				}
			}
		}

		public int boxLevel
		{
			get
			{
				return PlayerPrefs.GetInt("boxLevel" + base.name, DataProvider.instance.gameData.currentBoxLevel);
			}
			set
			{
				PlayerPrefs.SetInt("boxLevel" + base.name, value);
			}
		}

		public Unit unit
		{
			get
			{
				if (PlayerPrefs.HasKey("slotUnitID" + base.name))
				{
					return UnitManager.instance.units[PlayerPrefs.GetInt("slotUnitID" + base.name)];
				}
				if (this.TUT_UID > -1 && this.type == SlotType.Unit)
				{
					return UnitManager.instance.units[this.TUT_UID];
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					PlayerPrefs.DeleteKey("slotUnitID" + base.name);
				}
				else
				{
					PlayerPrefs.SetInt("slotUnitID" + base.name, value.id);
				}
			}
		}

		public BoxType boxType
		{
			get
			{
				if (PlayerPrefs.HasKey("slotBox" + base.name))
				{
					if (PlayerPrefs.GetString("slotBox" + base.name) == "Normal")
					{
						this._boxType = BoxType.Normal;
					}
					else if (PlayerPrefs.GetString("slotBox" + base.name) == "Mystry")
					{
						this._boxType = BoxType.Mystry;
					}
					else if (PlayerPrefs.GetString("slotBox" + base.name) == "FreeVideo")
					{
						this._boxType = BoxType.FreeVideo;
					}
					else if (PlayerPrefs.GetString("slotBox" + base.name) == "UnitSpawner")
					{
						this._boxType = BoxType.UnitSpawner;
					}
					else
					{
						this._boxType = BoxType.Gift;
					}
				}
				else
				{
					this._boxType = BoxType.Normal;
					PlayerPrefs.SetString("slotBox" + base.name, "Normal");
				}
				return this._boxType;
			}
			set
			{
				this._boxType = value;
				if (this._boxType == BoxType.Normal)
				{
					PlayerPrefs.SetString("slotBox" + base.name, "Normal");
				}
				else if (this._boxType == BoxType.Mystry)
				{
					PlayerPrefs.SetString("slotBox" + base.name, "Mystry");
				}
				else if (this._boxType == BoxType.FreeVideo)
				{
					PlayerPrefs.SetString("slotBox" + base.name, "FreeVideo");
				}
				else if (this._boxType == BoxType.UnitSpawner)
				{
					PlayerPrefs.SetString("slotBox" + base.name, "UnitSpawner");
				}
				else
				{
					PlayerPrefs.SetString("slotBox" + base.name, "Gift");
				}
			}
		}

		public void Reset()
		{
			this.type = SlotType.Empty;
			this.unit = null;
			this.boxType = BoxType.Normal;
		}
	}
}

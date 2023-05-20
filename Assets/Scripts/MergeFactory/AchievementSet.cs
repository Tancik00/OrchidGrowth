using System;
using System.Collections.Generic;
using UnityEngine;

namespace MergeFactory
{
	[CreateAssetMenu(menuName = "MergeFactory/AchievementSet")]
	public class AchievementSet : ScriptableObject
	{
		public AchievementType TYPE;

		public List<Achievement> ACHIEVEMENTS;

		public int CURRENTACHIEVEMENTINDEX
		{
			get
			{
				int i;
				for (i = 0; i < this.ACHIEVEMENTS.Count; i++)
				{
					if (!this.ACHIEVEMENTS[i].isACHIEVED)
					{
						break;
					}
				}
				return i;
			}
		}

		public bool isACHIEVEMENTSETCOMPLETE
		{
			get
			{
				bool result = true;
				for (int i = 0; i < this.ACHIEVEMENTS.Count; i++)
				{
					if (!this.ACHIEVEMENTS[i].isACHIEVED)
					{
						result = false;
						break;
					}
				}
				return result;
			}
		}
	}
}

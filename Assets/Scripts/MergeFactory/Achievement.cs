using System;
using UnityEngine;

namespace MergeFactory
{
	[CreateAssetMenu(menuName = "MergeFactory/Achievement")]
	public class Achievement : ScriptableObject
	{
		public float ID;

		public AchievementType TYPE;

		public string NAME;

		public string DESCRIPTION;

		public float TARGET;

		public float REWARD;

		public bool isACHIEVED
		{
			get
			{
				return PlayerPrefs.GetInt("isAchievement" + base.name + "Achieved", 0) == 1;
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("isAchievement" + base.name + "Achieved", 1);
				}
				else
				{
					PlayerPrefs.SetInt("isAchievement" + base.name + "Achieved", 0);
				}
			}
		}

		public bool isREPORTED
		{
			get
			{
				return PlayerPrefs.GetInt("isAchievement" + base.name + "Reported", 0) == 1;
			}
			set
			{
				if (value)
				{
					PlayerPrefs.SetInt("isAchievement" + base.name + "Reported", 1);
				}
				else
				{
					PlayerPrefs.SetInt("isAchievement" + base.name + "Reported", 0);
				}
			}
		}
	}
}

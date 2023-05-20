using System;
using UnityEngine;

namespace TapticPlugin
{
	public class SampleUI : MonoBehaviour
	{
		public void OnNotificationClick(int index)
		{
			TapticManager.Notification((NotificationFeedback)index);
			UnityEngine.Debug.LogFormat("notification {0}", new object[]
			{
				index
			});
		}

		public void OnImpactClick(int index)
		{
			TapticManager.Impact((ImpactFeedback)index);
			UnityEngine.Debug.LogFormat("impact {0}", new object[]
			{
				index
			});
		}

		public void OnSelectionUpdate()
		{
			TapticManager.Selection();
			UnityEngine.Debug.Log("selection");
		}
	}
}

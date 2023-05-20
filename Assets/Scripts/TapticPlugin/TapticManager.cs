using System;

namespace TapticPlugin
{
	public static class TapticManager
	{
		public static void Notification(NotificationFeedback feedback)
		{
			TapticManager._unityTapticNotification((int)feedback);
		}

		public static void Impact(ImpactFeedback feedback)
		{
			TapticManager._unityTapticImpact((int)feedback);
		}

		public static void Selection()
		{
			TapticManager._unityTapticSelection();
		}

		public static bool IsSupport()
		{
			return TapticManager._unityTapticIsSupport();
		}

		private static void _unityTapticNotification(int type)
		{
		}

		private static void _unityTapticSelection()
		{
		}

		private static void _unityTapticImpact(int style)
		{
		}

		private static bool _unityTapticIsSupport()
		{
			return false;
		}
	}
}

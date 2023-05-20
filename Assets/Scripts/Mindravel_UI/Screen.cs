using System;
using UnityEngine;

namespace Mindravel.UI
{
	public class Screen : MonoBehaviour
	{
		public ScreenName screenName;

		public bool fullScreen;

		public bool firstScreen;

		private void OnEnable()
		{
            /*
			if (MRUtilities.Instance)
			{
				MRUtilities.Instance.LogScreen(this.screenName.ToString());
			}
			*/
		}
	}
}

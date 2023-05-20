using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mindravel.UI
{
	public class MRMessagePanel : MonoBehaviour
	{
		public Text text_title;

		public Text text_message;

		public void ShowMessage(string p_title, string p_message)
		{
			this.text_title.text = p_title;
			this.text_message.text = p_message;
		}

		public void Button_OkayClicked()
		{
			//MRUtilities.Instance.HideMessagePopUp();
		}
	}
}

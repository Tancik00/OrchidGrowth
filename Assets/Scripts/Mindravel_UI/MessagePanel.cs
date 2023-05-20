using System;
using UnityEngine;
using UnityEngine.UI;

namespace Mindravel.UI
{
	public class MessagePanel : MonoBehaviour
	{
		public delegate void ConfirmationActions();

		public Text title_Text;

		public Text message_Text;

		public GameObject defaultButton;

		public GameObject textButton;

		public Text button_Text;

		public GameObject backButton;

		public GameObject rays;

		private MessagePanel.ConfirmationActions confirmedAction;

		private MessagePanel.ConfirmationActions cancelAction;

		public void ShowMessage(string title, string message, string buttonText = "", MessagePanel.ConfirmationActions _confirmedAction = null, MessagePanel.ConfirmationActions _cancelAction = null, bool showRays = false)
		{
			this.title_Text.text = title;
			this.message_Text.text = message;
			if (buttonText == string.Empty)
			{
				this.defaultButton.SetActive(true);
				this.backButton.SetActive(false);
				this.textButton.SetActive(false);
			}
			else
			{
				this.defaultButton.SetActive(false);
				this.backButton.SetActive(true);
				this.button_Text.text = buttonText;
				this.textButton.SetActive(true);
			}
			this.confirmedAction = _confirmedAction;
			this.cancelAction = _cancelAction;
			if (showRays)
			{
				this.rays.SetActive(true);
			}
			else
			{
				this.rays.SetActive(false);
			}
		}

		public void Button_Confirm()
		{
			if (this.confirmedAction != null)
			{
				this.confirmedAction();
			}
			else
			{
				GUIManager.Instance.Back();
			}
		}

		public void Button_Cancel()
		{
			if (this.cancelAction != null)
			{
				this.cancelAction();
			}
			else
			{
				GUIManager.Instance.Back();
			}
		}
	}
}

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TapticPlugin
{
	[RequireComponent(typeof(Button))]
	public class TapticButton : MonoBehaviour
	{
		public enum Feedback
		{
			NotificationSuccess,
			NotificationWarning,
			NorificationError,
			ImpactLight,
			ImpactMidium,
			ImpactHeavy,
			Selection
		}

		public TapticButton.Feedback feedback;

		private void OnEnable()
		{
			Button component = base.GetComponent<Button>();
			component.onClick.AddListener(new UnityAction(this.LoadScene));
		}

		private void OnDisable()
		{
			Button component = base.GetComponent<Button>();
			component.onClick.RemoveListener(new UnityAction(this.LoadScene));
		}

		private void LoadScene()
		{
			switch (this.feedback)
			{
			case TapticButton.Feedback.NotificationSuccess:
				TapticManager.Notification(NotificationFeedback.Success);
				break;
			case TapticButton.Feedback.NotificationWarning:
				TapticManager.Notification(NotificationFeedback.Warning);
				break;
			case TapticButton.Feedback.NorificationError:
				TapticManager.Notification(NotificationFeedback.Error);
				break;
			case TapticButton.Feedback.ImpactLight:
				TapticManager.Impact(ImpactFeedback.Light);
				break;
			case TapticButton.Feedback.ImpactMidium:
				TapticManager.Impact(ImpactFeedback.Midium);
				break;
			case TapticButton.Feedback.ImpactHeavy:
				TapticManager.Impact(ImpactFeedback.Heavy);
				break;
			case TapticButton.Feedback.Selection:
				TapticManager.Selection();
				break;
			}
		}
	}
}

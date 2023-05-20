using Mindravel.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MergeFactory
{
	public class WelcomeBackScreen : MonoBehaviour
	{
		public delegate void ConfirmationActions();

		public Text coinText;
		public Text doubleCoinText;

		private WelcomeBackScreen.ConfirmationActions confirmedAction;
		private WelcomeBackScreen.ConfirmationActions cancelAction;

		private bool AdsEnabled;

        private void Awake()
        {
			AdsEnabled = true;

#if UNITY_EDITOR
			AdsEnabled = false;
#endif
		}



        public void ShowMessage(float earned, WelcomeBackScreen.ConfirmationActions _confirmedAction = null, WelcomeBackScreen.ConfirmationActions _cancelAction = null)
		{
			this.coinText.text = "YOU EARNED : " + earned.ToShortHandString();
			this.doubleCoinText.text = (earned * 2f).ToShortHandString();
			this.confirmedAction = _confirmedAction;
			this.cancelAction = _cancelAction;
		}

		public void Button_Confirm()
		{
			if (AdsEnabled)
			{
				if (ads.instance.rewardedAvailable)
					ads.instance.ShowRewardVideo("Welcome2X_REWARDED");
				else
				{
					GUIManager.Instance.OpenScreenExplicitly(ScreenName.MessagePanel);
					GUIManager.Instance.CURRENTPANEL.GetComponent<MessagePanel>().ShowMessage("VIDEO NOT AVAILABLE", "TRY AGAIN LATER !", string.Empty, null, null, false);
				}
			}
			else
				WelcomeDoubleReward();
		}

		public void Button_Cancel()
		{
			if (this.cancelAction != null)
			{
				this.cancelAction();
				print("Cancel Action");
			}
			else
			{
				GUIManager.Instance.Back();
				print("Back Action");
			}
		}


		public void WelcomeDoubleReward()
        {
			if (this.confirmedAction != null)
			{
				this.confirmedAction();
				print("Confirmed Action");
				//GUIManager.Instance.Back();
			}
			else
			{
				GUIManager.Instance.Back();
				print("Back Action");
			}
		}
	}
}

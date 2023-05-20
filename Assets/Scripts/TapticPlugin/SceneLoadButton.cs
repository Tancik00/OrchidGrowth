using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TapticPlugin
{
	[RequireComponent(typeof(Button))]
	public class SceneLoadButton : MonoBehaviour
	{
		public string sceneName;

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
			SceneManager.LoadScene(this.sceneName);
		}
	}
}

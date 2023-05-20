using Mindravel.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Script_AchievementUnlocked : MonoBehaviour
{
	private sealed class _Disable_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _Disable_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				this._current = new WaitForSeconds(1.8f);
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				if (GUIManager.Instance.CURRENTSCREENNAME == ScreenName.AchievementUnlocked)
				{
					GUIManager.Instance.Back();
				}
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}
	}

	public GameObject container;

	public GameObject overlay;

	public Text text_label_achievementName;

	private void OnEnable()
	{
		this.container.GetComponents<TweenPosition>()[0].ResetToBeginning();
		this.container.GetComponents<TweenPosition>()[0].PlayForward();
		this.container.GetComponents<TweenPosition>()[1].ResetToBeginning();
		this.container.GetComponents<TweenPosition>()[1].PlayForward();
		this.overlay.GetComponents<TweenAlpha>()[0].ResetToBeginning();
		this.overlay.GetComponents<TweenAlpha>()[0].PlayForward();
		this.overlay.GetComponents<TweenAlpha>()[1].ResetToBeginning();
		this.overlay.GetComponents<TweenAlpha>()[1].PlayForward();
		base.StartCoroutine(this.Disable());
	}

	public void SetAchivementName(string p_achName)
	{
		this.text_label_achievementName.text = p_achName;
	}

	private IEnumerator Disable()
	{
		return new Script_AchievementUnlocked._Disable_c__Iterator0();
	}
}

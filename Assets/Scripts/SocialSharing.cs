using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SocialSharing : MonoBehaviour
{
	public delegate void SocialSharingDelegate();

	private sealed class _Post_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal int _width___0;

		internal int _height___0;

		internal Texture2D _tex___0;

		internal string _packageNamePattern___0;

		internal SocialPlatformEnum p_socialPlatform;

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

		public _Post_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				//if (MRUtilities.Instance.socialSharePanel)
				{
					//MRUtilities.Instance.socialSharePanel.SetActive(true);
				}
				this._current = new WaitForEndOfFrame();
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				this._width___0 = Screen.width;
				this._height___0 = Screen.height;
				this._tex___0 = new Texture2D(this._width___0, this._height___0, TextureFormat.RGB24, false);
				this._tex___0.ReadPixels(new Rect(0f, 0f, (float)this._width___0, (float)this._height___0), 0, 0);
				this._tex___0.Apply();
				//if (MRUtilities.Instance.socialSharePanel)
				{
					//MRUtilities.Instance.socialSharePanel.SetActive(false);
				}
				this._packageNamePattern___0 = string.Empty;
				if (this.p_socialPlatform == SocialPlatformEnum.FACEBOOK)
				{
					this._packageNamePattern___0 = "facebook.katana";
					MR.Log("started intent");
				}
				else if (this.p_socialPlatform == SocialPlatformEnum.TWITTER)
				{
					this._packageNamePattern___0 = "twi";
				}
				else if (this.p_socialPlatform == SocialPlatformEnum.INSTAGRAM)
				{
					this._packageNamePattern___0 = "insta";
				}
				UnityEngine.Object.Destroy(this._tex___0);
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

	private static SocialSharing instance;

	private SocialSharing.SocialSharingDelegate socialSharingSuccessCallback;

	public static SocialSharing Instance
	{
		get
		{
			if (SocialSharing.instance == null)
			{
				SocialSharing.instance = new SocialSharing();
			}
			return SocialSharing.instance;
		}
	}

	private SocialSharing()
	{
	}

	private void HandleOnShareIntentCallback(bool status, string package)
	{
		MR.Log("CallBack , [HandleOnShareIntentCallback] " + status.ToString() + " " + package);
		this.socialSharingSuccessCallback();
	}

	public void ShareOnSocialPlatform(SocialSharing.SocialSharingDelegate p_socialSharingSuccessCallBack, string p_title, string p_message, SocialPlatformEnum p_socialPlatform)
	{
		this.socialSharingSuccessCallback = p_socialSharingSuccessCallBack;
		//if (MRUtilities.Instance)
		{
		//MRUtilities.Instance.StartCoroutine(this.Post(p_title, p_message, p_socialPlatform, null));
		}
	}

	private IEnumerator Post(string p_title, string p_message, SocialPlatformEnum p_socialPlatform, Texture2D p_texture = null)
	{
		SocialSharing._Post_c__Iterator0 _Post_c__Iterator = new SocialSharing._Post_c__Iterator0();
		_Post_c__Iterator.p_socialPlatform = p_socialPlatform;
		return _Post_c__Iterator;
	}
}

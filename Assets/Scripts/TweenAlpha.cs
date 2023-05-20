using System;
using UnityEngine;
using UnityEngine.UI;

public class TweenAlpha : UITweener
{
	[Range(0f, 1f), Space(15f)]
	public float from = 1f;

	[Range(0f, 1f)]
	public float to = 1f;

	private bool mCached;

	private SpriteRenderer mRect;

	private Material mMat;

	private SpriteRenderer mSr;

	private Text mText;

	private Image mImage;

	private CanvasGroup mCanvasGroup;

	[Obsolete("Use 'value' instead")]
	public float alpha
	{
		get
		{
			return this.value;
		}
		set
		{
			this.value = value;
		}
	}

	public float value
	{
		get
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mText != null)
			{
				return this.mText.color.a;
			}
			if (this.mImage != null)
			{
				return this.mImage.color.a;
			}
			if (this.mSr != null)
			{
				return this.mSr.color.a;
			}
			if (this.mCanvasGroup != null)
			{
				return this.mCanvasGroup.alpha;
			}
			return (!(this.mMat != null)) ? 1f : this.mMat.color.a;
		}
		set
		{
			if (!this.mCached)
			{
				this.Cache();
			}
			if (this.mImage != null)
			{
				Color color = this.mImage.color;
				color.a = value;
				this.mImage.color = color;
			}
			else if (this.mSr != null)
			{
				Color color2 = this.mSr.color;
				color2.a = value;
				this.mSr.color = color2;
			}
			else if (this.mMat != null)
			{
				Color color3 = this.mMat.color;
				color3.a = value;
				this.mMat.color = color3;
			}
			else if (this.mText != null)
			{
				Color color4 = this.mText.color;
				color4.a = value;
				this.mText.color = color4;
			}
			else if (this.mCanvasGroup != null)
			{
				this.mCanvasGroup.alpha = value;
			}
		}
	}

	private void Cache()
	{
		this.mCached = true;
		this.mCanvasGroup = base.GetComponent<CanvasGroup>();
		if (this.mCanvasGroup != null)
		{
			return;
		}
		this.mImage = base.GetComponent<Image>();
		if (this.mImage != null)
		{
			return;
		}
		this.mSr = base.GetComponent<SpriteRenderer>();
		if (this.mSr != null)
		{
			return;
		}
		this.mText = base.GetComponent<Text>();
		if (this.mText != null)
		{
			return;
		}
	}

	protected override void OnUpdate(float factor, bool isFinished)
	{
		this.value = Mathf.Lerp(this.from, this.to, factor);
	}

	public static TweenAlpha Begin(GameObject go, float duration, float alpha)
	{
		TweenAlpha tweenAlpha = UITweener.Begin<TweenAlpha>(go, duration);
		tweenAlpha.from = tweenAlpha.value;
		tweenAlpha.to = alpha;
		if (duration <= 0f)
		{
			tweenAlpha.Sample(1f, true);
			tweenAlpha.enabled = false;
		}
		return tweenAlpha;
	}

	public override void SetStartToCurrentValue()
	{
		this.from = this.value;
	}

	public override void SetEndToCurrentValue()
	{
		this.to = this.value;
	}
}

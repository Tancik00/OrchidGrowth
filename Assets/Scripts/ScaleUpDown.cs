using System;
using System.Collections;
using UnityEngine;

public class ScaleUpDown : MonoBehaviour
{
	private Vector3 bigScale = new Vector3(0.9f, 0.9f, 0.9f);

	private Vector3 normalScale = Vector3.one;

	private Hashtable ht;

	private void OnEnable()
	{
		this.ht = new Hashtable();
		this.ToBig();
	}

	private void ToBig()
	{
		this.ht.Clear();
		this.ht.Add("scale", this.bigScale);
		this.ht.Add("time", 0.5f);
		this.ht.Add("oncomplete", "ToBigDone");
		//this.ht.Add("easetype", iTween.EaseType.linear);
		//iTween.ScaleTo(base.gameObject, this.ht);
	}

	private void ToBigDone()
	{
		this.ToNormal();
	}

	private void ToNormal()
	{
		this.ht.Clear();
		this.ht.Add("scale", this.normalScale);
		this.ht.Add("time", 0.5f);
		this.ht.Add("oncomplete", "ToNormalDone");
		//this.ht.Add("easetype", iTween.EaseType.linear);
		//iTween.ScaleTo(base.gameObject, this.ht);
	}

	private void ToNormalDone()
	{
		this.ToBig();
	}
}

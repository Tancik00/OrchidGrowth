using System;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
	public Transform target;

	private void OnEnable()
	{
		base.transform.position = this.target.position;
	}
}

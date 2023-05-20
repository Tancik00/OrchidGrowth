using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float speed;

	private void Update()
	{
		base.transform.Rotate(new Vector3(0f, 0f, this.speed * Time.deltaTime));
	}
}

using System;
using UnityEngine;

public class ResetStoreGridPosition : MonoBehaviour
{
	public GameObject storeGrid;

	public GameObject tutorialObject;

	public GameObject targetObject;

	private void OnEnable()
	{
		this.storeGrid.transform.localPosition = new Vector3(0f, 0f, 0f);
		this.tutorialObject.transform.position = this.targetObject.transform.position;
	}
}

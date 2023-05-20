using MergeFactory;
using System;
using UnityEngine;

public class PosTempGridSlot : MonoBehaviour
{
	public Transform targetSlot;

	public bool handsOnly;

	private void OnEnable()
	{
		base.transform.position = Camera.main.WorldToScreenPoint(this.targetSlot.position);
		if (this.handsOnly)
		{
			return;
		}
		BoxManager.instance.SpawnBox(this.targetSlot.GetComponent<Slot>(), BoxType.Normal);
		this.targetSlot.GetComponent<Slot>().item.GetComponent<CanvasGroup>().alpha = 0f;
	}

	public void OnClick()
	{
		this.targetSlot.GetComponent<Slot>().item.GetComponent<CanvasGroup>().alpha = 1f;
		this.targetSlot.GetComponent<Slot>().item.GetComponent<Box>().BoxClick_Button();
		TutorialManager.instance.CompleteTask_TUT1();
	}
}

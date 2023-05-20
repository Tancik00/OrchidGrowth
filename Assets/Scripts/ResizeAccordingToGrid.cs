using MergeFactory;
using System;
using UnityEngine;

public class ResizeAccordingToGrid : MonoBehaviour
{
	private void OnEnable()
	{
		base.GetComponent<RectTransform>().sizeDelta = CustomGridLayout.instance.CURRENT_ELEMENTSIZE;
		base.GetComponent<RectTransform>().SetPivot(PivotPresets.MiddleCenter);
		base.GetComponent<RectTransform>().SetAnchor(AnchorPresets.MiddleCenter, 0, 0);
		base.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}
}

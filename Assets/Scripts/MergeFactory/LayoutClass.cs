using System;
using UnityEngine;

namespace MergeFactory
{
	[Serializable]
	public class LayoutClass
	{
		public int validChildCount;

		public Axis axis;

		public Constraint constraint;

		public int constraintCount;

		public Vector2 cellSize;

		public Vector2 itemSize;
		public float gridBubbleScale;
	}
}

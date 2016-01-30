using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class TextSpawnner : ComponentBehaviour
{
	public string[] Texts;
	public EntityBehaviour TextPrefab;

	public bool Random;
	public bool Loop;
	int textIndex;
	public int TextIndex { get { return textIndex; } set { textIndex = value; } }

	float counter;
	public float Counter { get { return counter; } set { counter = value; } }

	public float Frequency;
	public bool IsAlive { get { return Loop || (!Random && textIndex < Texts.Length); } }
	public bool IsDone { get { return !IsAlive; } }

	public Zone2DBase Zone;

	public MinMax ScaleRange;
	public MinMax RotateAngle;

	public EntityBehaviour LookTarget;

	public Gradient TextColorGradiant;

	public string NextText()
	{
		if (Random)
			return Texts.GetRandom();
		else
		{
			string nextText = Texts[textIndex];
			textIndex++;
			if (Loop)
				textIndex %= Texts.Length;
			return nextText;
		}
	}
}
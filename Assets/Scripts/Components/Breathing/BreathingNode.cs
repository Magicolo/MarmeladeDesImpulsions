using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class BreathingNode : ComponentBehaviour
{
	public float BreatheInTime = 1f;
	public float HoldTime = 1f;
	public float BreatheOutTime = 1f;

	public float BreatheEmptyCircleScale = 1f;
	public float BreatheFullCircleScale = 2f;

	public float counter = 0f;

	public BreathingNode NextNode;

	public Transform IndicatorTransform;

	public float HoldTimeTotal { get { return BreatheInTime + HoldTime; } }
	public float BreatheOutTimeTotal { get { return BreatheInTime + HoldTime + BreatheOutTime; } }
}
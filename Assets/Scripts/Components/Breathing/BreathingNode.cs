using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class BreathingNode : ComponentBehaviour
{
	public bool actif;

	public float Activatingtime = 1;
	public float BreatheInTime = 1f;
	public float HoldTime = 1f;
	public float BreatheOutTime = 1f;
	public float BreatheOutHoldTime = 1f;

	public float BreatheEmptyCircleScale = 1f;
	public float BreatheFullCircleScale = 2f;

	public float counter = 0f;

	public BreathingNode NextNode;

	public Transform IndicatorTransform;
	public SpriteRenderer Sprite;
	public Color SpriteActivated;
	public Color SpriteDeactivated;

	public float BreatheInTimeTotal { get { return Activatingtime + BreatheInTime; } }
	public float HoldTimeTotal { get { return Activatingtime + BreatheInTime + HoldTime; } }
	public float BreatheOutTimeTotal { get { return Activatingtime + BreatheInTime + HoldTime + BreatheOutTime; } }
	public float BreatheOutHoldTimeTotal { get { return Activatingtime + BreatheInTime + HoldTime + BreatheOutTime + BreatheOutHoldTime; } }
}
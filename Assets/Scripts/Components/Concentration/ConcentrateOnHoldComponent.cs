using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ConcentrateOnHoldComponent : ComponentBehaviour
{
	public float ConcentrationGain = 1f;

	public bool Hovering
	{
		get { return hovering; }
		set { hovering = value; }
	}
	public bool Clicking
	{
		get { return clicking; }
		set { clicking = value; }
	}

	bool hovering;
	bool clicking;
}
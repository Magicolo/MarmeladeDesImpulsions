using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class SpeedOscillator : ComponentBehaviour
{
	public float Amplitude = 1f;
	public float Center;
	public float Frequency = 1f;
}
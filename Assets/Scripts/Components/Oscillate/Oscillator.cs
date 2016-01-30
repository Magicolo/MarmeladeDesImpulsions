using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public abstract class Oscillator : ComponentBehaviour
{
	public abstract void Oscillate(float time);
}
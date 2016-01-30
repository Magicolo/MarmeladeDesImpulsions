using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(LifeTimeComponent))]
public class TextMeshColorOverLifeTime : ComponentBehaviour
{
	public Gradient ColorGradient;
	public TextMesh TargetTextMesh;
}
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class ConcentrationColorizerComponent : ComponentBehaviour
{
	public SpriteRenderer Renderer;
	public MinMax Concentration = new MinMax(0f, 1f);
	public Gradient Color;
	public float FadeSpeed = 3f;
}
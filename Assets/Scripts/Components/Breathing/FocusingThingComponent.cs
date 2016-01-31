using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FocusingThingComponent : ComponentBehaviour
{
	public CircleZone zone;
	public SpriteRenderer Sprite;
	public Color FocusedColor;
	public Color UnfocusedColor;

	public float precisionNeeded;
	public float damage;
	public float regen;
}

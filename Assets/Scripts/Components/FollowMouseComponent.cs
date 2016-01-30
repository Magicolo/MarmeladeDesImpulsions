using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class FollowMouseComponent : ComponentBehaviour
{
	public Transform Transform;
	public TimeComponent Time;
}
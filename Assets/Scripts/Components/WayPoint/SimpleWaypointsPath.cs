using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SimpleWaypointsPath : ComponentBehaviour
{
	public Transform[] Waypoints;
	public bool Loop = true;
}
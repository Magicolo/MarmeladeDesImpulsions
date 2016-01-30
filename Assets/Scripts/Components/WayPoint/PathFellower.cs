using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class PathFellower : ComponentBehaviour
{
	public EntityBehaviour Path;
	public int CurrentPathIndexTarget = -1;

	public float Speed = 1f;

}
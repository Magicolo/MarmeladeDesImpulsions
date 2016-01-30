using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

[RequireComponent(typeof(MouvementSpeedComponent))]
public class PathFellower : ComponentBehaviour
{
	public EntityBehaviour Path;
	public int CurrentPathIndexTarget = -1;
	public bool random;

}
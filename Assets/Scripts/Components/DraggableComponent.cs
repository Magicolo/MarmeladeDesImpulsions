using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class DraggableComponent : ComponentBehaviour
{
	public bool Locked { get; set; }
	public bool Dragging { get; set; }
}
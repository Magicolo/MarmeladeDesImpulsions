﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SwitchSceneOnEventComponent : ComponentBehaviour
{
	public Events Event;
	public UIEvents UIEvent;
	public BehaviourEvents BehaviourEvent;
	public string Scene;
}
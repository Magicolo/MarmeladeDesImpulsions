using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TimeScaleSetOnEventComponent : ComponentBehaviour
{
	public TimeManager.TimeChannels TimeChanel;
	public float TimeScaleOnEvent;
	public Events OnEvents;
}
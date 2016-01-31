using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TimeScaleSetOnEventComponent : ComponentBehaviour
{
	[Serializable]
	public struct EventTimeScaler
	{
		public Events OnEvents;
		public BehaviourEvents OnBehaviourEvents;

		public TimeManager.TimeChannels TimeChanel;
		public float TimeScaleOnEvent;
	}

	[InitializeContent]
	public EventTimeScaler[] Events = new EventTimeScaler[0];
}
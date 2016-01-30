using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ConcentrationSoundsOnEventComponent : ComponentBehaviour
{
	[Serializable]
	public class SoundData
	{
		public AudioSettingsBase Sound;
		public MinMax Scale;
	}

	public BehaviourEvents Event;
	[InitializeContent]
	public SoundData[] Sounds = new SoundData[0];
}
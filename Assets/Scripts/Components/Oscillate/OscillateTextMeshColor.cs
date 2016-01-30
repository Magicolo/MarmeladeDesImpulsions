using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;


public class OscillateTextMeshColor : Oscillator
{
	public TextMesh TargetTextMesh;
	public Color Frequency;
	public Color Amplitude;
	public Color Center;
	public Channels Channels;

	public override void Oscillate(float time)
	{
		TargetTextMesh.color = TargetTextMesh.color.Oscillate(Frequency, Amplitude, Center, 0, time, Channels);
	}
}
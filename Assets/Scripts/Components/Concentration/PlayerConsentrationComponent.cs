using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class PlayerConsentrationComponent : ComponentBehaviour
{
	public float Consentration = 50;
	public float ConsentrationNeededToWin = 100;
	public CircleZone zone;

	public float T { get { return (Consentration / ConsentrationNeededToWin).Clamp(0, ConsentrationNeededToWin); } }
}
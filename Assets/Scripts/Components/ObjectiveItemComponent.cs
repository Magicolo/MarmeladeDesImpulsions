using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ObjectiveItemComponent : ComponentBehaviour
{
	public enum Types
	{
		None,
		CrystalGreen,
		CrystalRed,
		PlumeBlancheNoir,
		PlumeOrange,
		PlumeBleu,
		PlumeGroupe,
		Encens
	}

	public Types Type;
}
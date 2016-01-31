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
		Encens,
		Candle,
		Powder,
		Feather,
		Rock,
		Amulet,
		Bell,
		Gong,
		Voice,
		Dance,
		Horn,
		Paw,
		Parchment,
		Skin,
		Dagger,
		Phial,
		DreamCatcher,
		Branch
	}

	public Types Type;
}
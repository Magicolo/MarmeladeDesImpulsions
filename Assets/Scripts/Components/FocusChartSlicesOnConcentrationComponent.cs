using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FocusChartSlicesOnConcentrationComponent : ComponentBehaviour
{
	public FocusChartRenderer FocusChart;
	public PlayerConsentrationComponent PlayerConcentration;

	public MinMax Slides;
	public MinMax FillRatio;
}
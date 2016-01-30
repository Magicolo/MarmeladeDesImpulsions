using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class FocusChartSlicesOnConcentrationSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(FocusChartSlicesOnConcentrationComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var focusChartComponent = entity.GetComponent<FocusChartSlicesOnConcentrationComponent>();
		var player = focusChartComponent.PlayerConcentration;
		var focusChart = focusChartComponent.FocusChart;
		var minMax = focusChartComponent.Slides;

		var slides = 0;
		if (player.Consentration >= player.ConsentrationNeededToWin)
			slides = (int)minMax.Min;
		else if (player.Consentration <= 0)
			slides = (int)minMax.Max;
		else
			slides = (int)player.Consentration.Scale(0, player.ConsentrationNeededToWin, minMax.Max, minMax.Min);
		focusChart.Slices = slides;
	}
}

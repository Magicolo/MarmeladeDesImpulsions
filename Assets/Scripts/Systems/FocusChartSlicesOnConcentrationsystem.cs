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
		var fillRange = focusChartComponent.FillRatio;

		var slides = 0f;
		var fillRatio = 0.5f;
		if (player.Consentration >= player.ConsentrationNeededToWin)
		{
			slides = (int)minMax.Min;
			fillRatio = fillRange.Max;
		}

		else if (player.Consentration <= 0)
			slides = (int)minMax.Max;
		else
		{
			slides = (int)player.Consentration.Scale(0, player.ConsentrationNeededToWin, minMax.Max, minMax.Min);
			fillRatio = player.Consentration.Scale(0, player.ConsentrationNeededToWin, fillRange.Min, fillRange.Max);
		}

		focusChart.Slices = (int)slides;
		focusChart.FillRatio = fillRatio;
	}
}

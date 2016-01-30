using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class ConcentrationColorizerSystem : SystemBase, IUpdateable
{
	[Inject]
	IPuzzleLevel Level = null;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(TimeComponent),
			typeof(ConcentrationColorizerComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var time = entity.GetComponent<TimeComponent>();
		var colorizer = entity.GetComponent<ConcentrationColorizerComponent>();
		var value = Mathf.Clamp01(Level.Concentration * (colorizer.Concentration.Max - colorizer.Concentration.Min) + colorizer.Concentration.Min);
		var color = colorizer.Color.Evaluate(value);

		colorizer.Renderer.color = Color.Lerp(colorizer.Renderer.color, color, time.DeltaTime * colorizer.FadeSpeed);
	}
}

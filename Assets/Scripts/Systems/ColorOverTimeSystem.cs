using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class ColorOverTimeSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(TextMeshColorOverLifeTime)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var colorOverTime = entity.GetComponent<TextMeshColorOverLifeTime>();
		var lifeTime = entity.GetComponent<LifeTimeComponent>();
		var player = EntityManager.Entities.Filter(typeof(PlayerConsentrationComponent))[0].GetComponent<PlayerConsentrationComponent>();
		var levelData = EntityManager.Entities.Filter(typeof(LevelDataComponent))[0].GetComponent<LevelDataComponent>();

		Color newColor = colorOverTime.ColorGradient.Evaluate(lifeTime.TimeRatio);
		Color color2 = levelData.TextColorOverConcentration.Evaluate(player.T);
		colorOverTime.TargetTextMesh.color = Color.Lerp(newColor, color2, player.T);


	}
}

using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TextSpawnerSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(TextSpawnner),
			typeof(TimeComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var textSpawnner = entity.GetComponent<TextSpawnner>();
		var time = entity.GetComponent<TimeComponent>();
		if (textSpawnner.IsDone) return;

		textSpawnner.Counter -= time.DeltaTime;

		if (textSpawnner.Counter <= 0)
		{
			textSpawnner.Counter = textSpawnner.Frequency;
			spawnText(textSpawnner);
		}
	}

	private void spawnText(TextSpawnner textSpawnner)
	{
		var go = EntityManager.CreateEntity(textSpawnner.TextPrefab);

		var text = go.GetComponentInChildren<TextMesh>();
		if (text)
			text.text = textSpawnner.NextText();

		go.transform.position = textSpawnner.Zone.GetRandomWorldPoint();
		float scale = textSpawnner.ScaleRange.GetRandom(ProbabilityDistributions.Normal);
		text.transform.localScale = new Vector3(scale, scale, 1);

		if (textSpawnner.LookTarget != null)
		{
			float angle = Vector3.Angle(textSpawnner.LookTarget.transform.position, go.transform.position);
			text.transform.Rotate(angle, Axes.Z);
		}

		text.transform.Rotate(textSpawnner.RotateAngle.GetRandom(ProbabilityDistributions.Normal), Axes.Z);

		var colorOverTime = go.GetComponent<TextMeshColorOverLifeTime>();
		colorOverTime.ColorGradient = textSpawnner.TextColorGradiant;
	}
}

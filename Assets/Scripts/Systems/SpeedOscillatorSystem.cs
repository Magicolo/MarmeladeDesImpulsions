using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class SpeedOscillatorSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(SpeedOscillator),
			typeof(MouvementSpeedComponent),
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
		var oscillator = entity.GetComponent<SpeedOscillator>();
		var mouvementSpeed = entity.GetComponent<MouvementSpeedComponent>();
		var time = entity.GetComponent<TimeComponent>();
		mouvementSpeed.Speed = oscillator.Center + oscillator.Amplitude * Mathf.Sin(oscillator.Frequency * time.Time);
	}
}

using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class OscillateColorSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(Oscillator),
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
		var time = entity.GetComponent<TimeComponent>();
		var oscillators = entity.GetComponents<Oscillator>();
		for (int i = 0; i < oscillators.Count; i++)
		{
			oscillators[i].Oscillate(time.Time);
		}
	}
}

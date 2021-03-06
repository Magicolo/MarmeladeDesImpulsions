﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class TriggerConcentrationEventSystem : SystemBase, IUpdateable
{
	[Inject]
	IPuzzleLevel level = null;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(TriggerConcentrationEventComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var trigger = entity.GetComponent<TriggerConcentrationEventComponent>();

		if (level.Concentration >= trigger.Threshold)
			EventManager.Trigger(trigger.Event, entity);
	}
}

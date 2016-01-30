using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class TimeScaleSetOnEventSystem : SystemBase
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(TimeScaleSetOnEventComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();
		EventManager.SubscribeAll((Action<Events, IEntity>)OnEvent);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();
		EventManager.UnsubscribeAll((Action<Events, IEntity>)OnEvent);
	}

	void OnEvent(Events identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var timeScaleSetter = entity.GetComponent<TimeScaleSetOnEventComponent>();
		if (timeScaleSetter.OnEvents.HasAny(identifier))
		{
			TimeManager.SetTimeScale(timeScaleSetter.TimeChanel, timeScaleSetter.TimeScaleOnEvent);
		}
	}
}

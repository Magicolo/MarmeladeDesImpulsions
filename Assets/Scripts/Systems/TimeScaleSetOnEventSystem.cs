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
		EventManager.SubscribeAll((Action<BehaviourEvents, IEntity>)OnBehaviourEvents);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();
		EventManager.UnsubscribeAll((Action<Events, IEntity>)OnEvent);
		EventManager.UnsubscribeAll((Action<BehaviourEvents, IEntity>)OnBehaviourEvents);
	}
	void OnBehaviourEvents(BehaviourEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var timeScaleSetter = entity.GetComponent<TimeScaleSetOnEventComponent>();
		for (int i = 0; i < timeScaleSetter.Events.Length; i++)
		{
			var scalerEvent = timeScaleSetter.Events[i];

			if (scalerEvent.OnBehaviourEvents.HasAny(identifier))
				TimeManager.SetTimeScale(scalerEvent.TimeChanel, scalerEvent.TimeScaleOnEvent);
		}

	}

	void OnEvent(Events identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var timeScaleSetter = entity.GetComponent<TimeScaleSetOnEventComponent>();
		for (int i = 0; i < timeScaleSetter.Events.Length; i++)
		{
			var scalerEvent = timeScaleSetter.Events[i];

			if (scalerEvent.OnEvents.HasAny(identifier))
				TimeManager.SetTimeScale(scalerEvent.TimeChanel, scalerEvent.TimeScaleOnEvent);
		}
	}


}

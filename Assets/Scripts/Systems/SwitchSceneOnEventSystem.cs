using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class SwitchSceneOnEventSystem : SystemBase
{
	[Inject]
	GameManager GameManager = null;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(SwitchSceneOnEventComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();

		EventManager.SubscribeAll((Action<BehaviourEvents, IEntity>)OnEvent);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.UnsubscribeAll((Action<BehaviourEvents, IEntity>)OnEvent);
	}

	void OnEvent(BehaviourEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var switcher = entity.GetComponent<SwitchSceneOnEventComponent>();

		if (switcher.Event.HasAll(identifier))
			GameManager.LoadScene(switcher.Scene);
	}
}

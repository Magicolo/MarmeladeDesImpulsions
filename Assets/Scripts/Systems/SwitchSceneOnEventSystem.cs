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
	GameManager gameManager = null;

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

		EventManager.SubscribeAll((Action<Events, IEntity>)OnEvent);
		EventManager.SubscribeAll((Action<UIEvents, IEntity>)OnUIEvent);
		EventManager.SubscribeAll((Action<BehaviourEvents, IEntity>)OnBehaviourEvent);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.UnsubscribeAll((Action<Events, IEntity>)OnEvent);
		EventManager.UnsubscribeAll((Action<UIEvents, IEntity>)OnUIEvent);
		EventManager.UnsubscribeAll((Action<BehaviourEvents, IEntity>)OnBehaviourEvent);
	}

	void OnEvent(Events identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var switcher = entity.GetComponent<SwitchSceneOnEventComponent>();

		if (switcher.Event.HasAll(identifier))
			gameManager.LoadScene(switcher.Scene);
	}

	void OnUIEvent(UIEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var switcher = entity.GetComponent<SwitchSceneOnEventComponent>();

		if (switcher.UIEvent.HasAll(identifier))
			gameManager.LoadScene(switcher.Scene);
	}

	void OnBehaviourEvent(BehaviourEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var switcher = entity.GetComponent<SwitchSceneOnEventComponent>();

		if (switcher.BehaviourEvent.HasAll(identifier))
			gameManager.LoadScene(switcher.Scene);
	}
}

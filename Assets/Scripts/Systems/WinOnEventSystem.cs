using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class WinOnEventSystem : SystemBase
{
	[Inject]
	GameManager gameManager = null;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(WinOnEventComponent)
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

		var win = entity.GetComponent<WinOnEventComponent>();

		if (win.Event.HasAll(identifier))
			gameManager.LevelSuccess();
	}
}

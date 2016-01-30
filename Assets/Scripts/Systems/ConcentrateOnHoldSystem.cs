using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;
using Zenject;

public class ConcentrateOnHoldSystem : SystemBase, IUpdateable
{
	[Inject]
	IPuzzleLevel Level;

	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(ConcentrateOnHoldComponent)
		});
	}

	public override void OnActivate()
	{
		base.OnActivate();

		EventManager.SubscribeAll((Action<UIEvents, IEntity>)OnEvent);
	}

	public override void OnDeactivate()
	{
		base.OnDeactivate();

		EventManager.UnsubscribeAll((Action<UIEvents, IEntity>)OnEvent);
	}

	public void Update()
	{
		Level.Concentration = Mathf.Max(Level.Concentration - TimeManager.World.DeltaTime * 0.1f, 0f);

		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var concentrate = entity.GetComponent<ConcentrateOnHoldComponent>();

		if (concentrate.Hovering && concentrate.Clicking)
			Level.Concentration += concentrate.ConcentrationGain * TimeManager.World.DeltaTime * 0.1f;
	}

	void OnEvent(UIEvents identifier, IEntity entity)
	{
		if (!Entities.Contains(entity))
			return;

		var concentrate = entity.GetComponent<ConcentrateOnHoldComponent>();

		if (identifier == UIEvents.OnPointerEnter)
			concentrate.Hovering = true;
		else if (identifier == UIEvents.OnPointerExit)
			concentrate.Hovering = false;
		else if (identifier == UIEvents.OnPointerDown)
			concentrate.Clicking = true;
		else if (identifier == UIEvents.OnPointerUp)
			concentrate.Clicking = false;
	}
}

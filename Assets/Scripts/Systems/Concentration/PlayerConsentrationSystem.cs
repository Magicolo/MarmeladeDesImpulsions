using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class PlayerConsentrationSystem : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(PlayerConsentrationComponent)
		});
	}

	public void Update()
	{
		for (int i = 0; i < Entities.Count; i++)
			Update(Entities[i]);
	}

	void Update(IEntity entity)
	{
		var playerConsentration = entity.GetComponent<PlayerConsentrationComponent>();
		if (playerConsentration.Consentration <= 0)
			EventManager.Trigger(Events.Lose, entity);
		if (playerConsentration.Consentration > playerConsentration.ConsentrationNeededToWin)
			EventManager.Trigger(Events.Win, entity);
	}
}

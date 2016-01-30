using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class CheckPlayerConsentrationSystem : SystemBase, IUpdateable
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
		var consentration = entity.GetComponent<PlayerConsentrationComponent>();
		if (consentration.Consentration <= 0)
			EventManager.Trigger(Events.Lose);
		else if (consentration.Consentration >= consentration.ConsentrationNeededToWin)
			EventManager.Trigger(Events.Win);
	}
}

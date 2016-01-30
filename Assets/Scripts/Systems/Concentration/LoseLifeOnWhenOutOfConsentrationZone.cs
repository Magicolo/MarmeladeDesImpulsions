using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Pseudo;

public class LoseLifeOnWhenOutOfConsentrationZone : SystemBase, IUpdateable
{
	public override IEntityGroup GetEntities()
	{
		return EntityManager.Entities.Filter(new[]
		{
			typeof(LoseLifeWhenOutOfConsentrationComponent),
			typeof(PlayerConsentrationComponent),
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
		var consentrationComponent = entity.GetComponent<PlayerConsentrationComponent>();
		var consentrationZone = consentrationComponent.zone.WorldCircle;
		var loseLife = entity.GetComponent<LoseLifeWhenOutOfConsentrationComponent>();
		var time = entity.GetComponent<TimeComponent>();

		if (time.DeltaTime <= 0) return;

		var zones = EntityManager.Entities.Filter(typeof(ConsentrationKeeperZone));

		bool colideWithSomething = false;
		foreach (var zone in zones.ToArray())
		{
			if (zone.GetComponent<ConsentrationKeeperZone>().zone.WorldCircle.Intersects(consentrationZone))
			{
				colideWithSomething = true;
				continue;
			}
		}

		if (colideWithSomething)
			consentrationComponent.Consentration += Time.deltaTime * loseLife.gainPerSecond;
		else
			consentrationComponent.Consentration -= Time.deltaTime * loseLife.amountPerSecond;
	}
}
